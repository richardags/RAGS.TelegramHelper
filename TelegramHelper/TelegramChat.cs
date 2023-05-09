using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramHelper
{
    public class TelegramChat
    {
        public class Message
        {
            public string text;
            public int id;
            public MessageType type;

            public Message() { }
            public Message(MessageType type, int id, string text)
            {
                this.type = type;
                this.id = id;
                this.text = text;
            }
        }
        public class From
        {
            public long id;
            public string username;
            public string firstName;
            public string lastName;

            public From(long id, string username, string firstName, string lastName)
            {
                this.id = id;
                this.username = username;
                this.firstName = firstName;
                this.lastName = lastName;
            }

            public string GetUsernameOrName()
            {
                if (username != null)
                {
                    return "@" + username;
                }
                else
                {
                    if (firstName != null && lastName != null)
                    {
                        return firstName + " " + lastName;
                    }
                    else if (firstName != null)
                    {
                        return firstName;
                    }
                    else if (lastName != null)
                    {
                        return lastName;
                    }
                    else
                    {
                        return "Ghost 👻";
                    }
                }
            }
        }

        public Message message;
        public From from;
        public long id;

        public TelegramChat() { }
        public TelegramChat(Telegram.Bot.Types.Message message)
        {
            this.message = new Message(
                message.Type,
                message.MessageId,
                message.Text
                );

            this.from = new From(
                message.From.Id,
                message.From.Username,
                message.From.FirstName,
                message.From.LastName
                );

            this.id = message.Chat.Id;
        }
        public TelegramChat(CallbackQuery callbackQuery)
        {
            this.message = new Message(
                callbackQuery.Message.Type,
                callbackQuery.Message.MessageId,
                callbackQuery.Data
                );

            this.from = new From(
                callbackQuery.From.Id,
                callbackQuery.From.Username,
                callbackQuery.From.FirstName,
                callbackQuery.From.LastName
                );

            this.id = callbackQuery.Message.Chat.Id;
        }
    }
}