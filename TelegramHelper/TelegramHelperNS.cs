using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramHelper
{
    public abstract class TelegramHelperNS
    {
        internal readonly TelegramManager manager = new();

        public async Task TelegramBotHelperUpdateHandler(ITelegramBotClient botClient, Update update,
            CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message || update.Type == UpdateType.EditedMessage)
            {
                await OnMessageOrEditedMessage(update);
            }
            else if (update.Type == UpdateType.CallbackQuery)
            {
                await OnCallbackQuery(update);
            }
        }
        public async Task TelegramBotHelperErrorHandler(ITelegramBotClient botClient, Exception exception,
            CancellationToken cancellationToken)
        {
            await OnTelegramBotHelperErrorHandlerEvent(botClient, exception, cancellationToken);
        }

        public abstract Task OnTelegramBotHelperErrorHandlerEvent(ITelegramBotClient botClient,
            Exception exception, CancellationToken cancellationToken);

        public async Task OnMessageOrEditedMessage(Update update)
        {
            TelegramChat chat = new(update.Message ?? update.EditedMessage);

            TelegramMember member = manager.FindMember(chat.from.id);

            if (chat.message.type == MessageType.Text)
            {
                //check if is pre-registered
                if (member != null)
                {
                    //update telegram info
                    member.user.Update(chat);

                    //check if is help info
                    if (chat.message.text.StartsWith("/"))
                    {
                        await OnMemberSlashMessageEvent(member, chat.message.text.Remove(0, 1));
                    }
                    else if (member.IsAuthenticated) //check if authenticated
                    {
                        if (member.IsWaintingAnswer())
                        {
                            await OnMemberAuthenticatedAnswerEvent(member, chat.message.text);
                        }
                        else
                        {
                            //default menu for authenticated members
                            await OnMemberAuthenticatedDefaultMenuEvent(member);
                        }
                    }
                    else //unauthenticated
                    {
                        if (member.IsWaintingAnswer())
                        {
                            await OnMemberUnauthenticatedAnswerEvent(member, chat.message.text);
                        }
                        else
                        {
                            //default menu for unauthenticated members
                            await OnMemberUnauthenticatedDefaultMenuEvent(member);
                        }
                    }
                }
                else
                {
                    //unregistered
                    await TelegramSendMenu.DefaultMenuAsync(OnSendTextMessageAsyncEvent, chat);
                }
            }
            else if (chat.message.type == MessageType.Document)
            {
                //check if is pre-registered
                if (member != null)
                {
                    //update telegram info
                    member.user.Update(chat);

                    //check if authenticated
                    if (member.IsAuthenticated)
                    {
                        if (member.IsWaintingAnswer())
                        {
                            await OnMemberAuthenticatedDocumentEvent(member, update.Message.Document);
                        }
                        else
                        {
                            //default menu for authenticated members
                            await OnMemberAuthenticatedDefaultMenuEvent(member);
                        }
                    }
                    else //unauthenticated
                    {
                        if (member.IsWaintingAnswer())
                        {
                            await OnMemberUnauthenticatedDocumentEvent(member, update.Message.Document);
                        }
                        else
                        {
                            //default menu for unauthenticated members
                            await OnMemberUnauthenticatedDefaultMenuEvent(member);
                        }
                    }
                }
                else
                {
                    //unregistered
                    await TelegramSendMenu.DefaultMenuAsync(OnSendTextMessageAsyncEvent, chat);
                }
            }
            else if (chat.message.type == MessageType.ChatMembersAdded)
            {
                Console.WriteLine("TelegramHelperNS.cs OnMessageOrEditedMessage() - warning not handled: ChatMembersAdded");
            }
            else if (chat.message.type == MessageType.ChatMemberLeft)
            {
                Console.WriteLine("TelegramHelperNS.cs OnMessageOrEditedMessage() - warning not handled: ChatMemberLeft");
            }
            else
            {
                Console.WriteLine("TelegramHelperNS.cs OnMessageOrEditedMessage() - warning not handled: " + chat.message.type);
            }
        }
        public async Task OnCallbackQuery(Update update)
        {
            TelegramChat chat = new(update.CallbackQuery);

            //delete last message
            await OnDeleteMessageEvent(chat.id, chat.message.id);

            TelegramMember member = manager.FindMember(chat.from.id);

            //check if is pre-registered
            if (member == null)
            {
                await Register(chat);
                return;
            }

            //update telegram info
            member.user.Update(chat);

            _ = Enum.TryParse(chat.message.text, out CommandType commandType);

            if (commandType != CommandType.NONE)
            {
                switch (commandType)
                {
                    case CommandType.ENGLISH:
                    case CommandType.PORTUGUESE:
                    case CommandType.SPANISH:
                        _ = Enum.TryParse(commandType.ToString(),
                            out TelegramLanguageEnum language);

                        member.language = language;
                        await TelegramSendMessage.LanguageUpdatedAsync(
                            OnSendTextMessageAsyncEvent, member);

                        if (member.IsAuthenticated)
                        {
                            await OnMemberAuthenticatedDefaultMenuEvent(member);
                        }
                        else
                        {
                            await OnMemberUnauthenticatedDefaultMenuEvent(member);
                        }
                        return;
                    case CommandType.CHANGE_LANGUAGE:
                        await TelegramSendMenu.DefaultMenuAsync(
                            OnSendTextMessageAsyncEvent, chat);
                        return;
                    case CommandType.LOGOUT:
                        manager.TryRemoveMember(member.user.from.id);
                        await OnMemberLogoutEvent(member);
                        return;
                    case CommandType.CANCEL_ANSWER:
                        member.waintingAnswer = 0;
                        await OnMemberCancelAnswerEvent(member);
                        return;
                }
            }

            if (member.IsAuthenticated)
            {
                await OnMemberAuthenticatedButtonEvent(member, chat.message.text);
            }
            else
            {
                await OnMemberUnauthenticatedButtonEvent(member, chat.message.text);
            }
        }

        internal async Task Register(TelegramChat chat)
        {
            _ = Enum.TryParse(chat.message.text,
                out TelegramLanguageEnum language);

            switch (language)
            {
                case TelegramLanguageEnum.ENGLISH:
                case TelegramLanguageEnum.PORTUGUESE:
                case TelegramLanguageEnum.SPANISH:

                    TelegramMember member = new()
                    {
                        user = new TelegramUser(chat),
                        language = language
                    };

                    //try adding new member
                    if (!manager.CheckIfExists(member.user.from.id))
                    {
                        manager.AddMember(member);
                    }

                    //check if already exists
                    if (member != null)
                    {
                        //show unauthenticated menu
                        await OnMemberUnauthenticatedDefaultMenuEvent(member);
                    }
                    else
                    {
                        // this statement never be called
                        Console.WriteLine("TelegramHelperNS.cs Register() - error falta: not handled correctly.");
                    }
                    break;
                default:
                    //message already deleted before, not need more
                    break;
            }
        }

        //events
        public abstract Task OnMemberCancelAnswerEvent(TelegramMember member);
        public abstract Task OnMemberLogoutEvent(TelegramMember member);

        public abstract Task OnMemberUnauthenticatedDefaultMenuEvent(TelegramMember member);
        public abstract Task OnMemberAuthenticatedDefaultMenuEvent(TelegramMember member);

        public abstract Task OnMemberSlashMessageEvent(TelegramMember member, string slash);

        public abstract Task OnMemberUnauthenticatedButtonEvent(TelegramMember member, string buttonData);
        public abstract Task OnMemberAuthenticatedButtonEvent(TelegramMember member, string buttonData);

        public abstract Task OnMemberUnauthenticatedAnswerEvent(TelegramMember member, string answer);
        public abstract Task OnMemberAuthenticatedAnswerEvent(TelegramMember member, string answer);

        public abstract Task OnMemberUnauthenticatedDocumentEvent(TelegramMember member, Document document);
        public abstract Task OnMemberAuthenticatedDocumentEvent(TelegramMember member, Document document);

        public abstract Task OnDeleteMessageEvent(long chatId, int messageId);
        public abstract Task OnSendTextMessageAsyncEvent(long chatId, string text, ParseMode parseMode,
            IReplyMarkup replyMarkup = null);

        public TelegramMember FindMember(long telegramId)
        {
            return manager.FindMember(telegramId);
        }
        public TelegramMember RemoveMember(long telegramId)
        {
            TelegramMember member = manager.FindMember(telegramId);

            if (member != null)
            {
                manager.TryRemoveMember(member.user.from.id);
                OnMemberLogoutEvent(member);

                return member;
            }
            else
            {
                return null;
            }
        }
    }
}