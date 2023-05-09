using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramHelper;

namespace Telegram
{
    public class TelegramNS : TelegramHelperNS
    {
        private readonly ITelegramBotClient telegram;
        private CancellationTokenSource cts;

        public TelegramNS(string telegramToken)
        {
            telegram = new TelegramBotClient(telegramToken);
        }

        public void StartReceiving()
        {
            cts = new CancellationTokenSource();

            telegram.StartReceiving(new DefaultUpdateHandler(
                base.TelegramBotHelperUpdateHandler, TelegramBotHelperErrorHandler),
                new ReceiverOptions { AllowedUpdates = { } },
                cts.Token);
        }
        public void StopReceiving()
        {
            cts.Cancel();
        }

        public override async Task OnMemberCancelAnswerEvent(TelegramMember member)
        {
            await Task.CompletedTask;
        }
        public override async Task OnMemberLogoutEvent(TelegramMember member)
        {
            await Task.CompletedTask;
        }

        public override async Task OnMemberUnauthenticatedDefaultMenuEvent(TelegramMember member)
        {
            await Task.CompletedTask;
        }
        public override async Task OnMemberAuthenticatedDefaultMenuEvent(TelegramMember member)
        {
            await Task.CompletedTask;
        }

        public override async Task OnMemberSlashMessageEvent(TelegramMember member, string slash)
        {
            await Task.CompletedTask;
        }

        public override async Task OnMemberUnauthenticatedButtonEvent(TelegramMember member, string buttonData)
        {
            await Task.CompletedTask;
        }
        public override async Task OnMemberAuthenticatedButtonEvent(TelegramMember member, string buttonData)
        {
            await Task.CompletedTask;
        }

        public override async Task OnMemberUnauthenticatedAnswerEvent(TelegramMember member, string answer)
        {
            await Task.CompletedTask;
        }
        public override async Task OnMemberAuthenticatedAnswerEvent(TelegramMember member, string answer)
        {
            await Task.CompletedTask;
        }

        public override async Task OnMemberUnauthenticatedDocumentEvent(TelegramMember member, Document document)
        {
            await Task.CompletedTask;
        }
        public override async Task OnMemberAuthenticatedDocumentEvent(TelegramMember member, Document document)
        {
            await Task.CompletedTask;
        }

        public override async Task OnDeleteMessageEvent(long chatId, int messageId)
        {
            try
            {
                await telegram.DeleteMessageAsync(chatId, messageId);
            }
            catch (Telegram.Bot.Exceptions.ApiRequestException exception)
            {
                Console.WriteLine("TelegramNS.cs OnDeleteMessageEvent() - error suppressed: " + exception);
            }
        }

        public override async Task OnSendTextMessageAsyncEvent(long chatId, string text, ParseMode parseMode, IReplyMarkup replyMarkup = null)
        {
            if (text.Length > 4096) //limit of api 4096 caracteres
            {
                throw new Exception("TelegramNS.cs OnSendTextMessageAsyncEvent() - error: max api text (4096), size: " + text.Length);
            }

            try
            {
                await telegram.SendTextMessageAsync(chatId, text, parseMode, replyMarkup: replyMarkup, cancellationToken: cts.Token);
            }
            catch (ApiRequestException exception)
            {
                Console.WriteLine("TelegramNS.cs OnSendTextMessageAsyncEvent() - error: ApiRequestException suppressed: " + exception);
            }
            catch (Exception exception)
            {
                Console.WriteLine("TelegramNS.cs OnSendTextMessageAsyncEvent() - error: Exception suppressed: " + exception);
            }
        }

        public override async Task OnTelegramBotHelperErrorHandlerEvent(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine("TelegramNS OnTelegramBotHelperErrorHandlerEvent() - fatal error: " + exception);
            await Task.CompletedTask;
        }
    }
}