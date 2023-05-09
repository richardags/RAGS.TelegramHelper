namespace TelegramHelper
{
    public class TelegramUser
    {
        public long chatId;
        public TelegramChat.From from;
        public TelegramChat.Message message;

        public TelegramUser(TelegramChat chat)
        {
            this.chatId = chat.id;
            this.from = chat.from;
            this.message = chat.message;
        }
        public void Update(TelegramChat chat)
        {
            this.chatId = chat.id;
            this.from = new TelegramChat.From(
                this.from.id,
                chat.from.username,
                chat.from.firstName,
                chat.from.lastName
                );
            this.message = chat.message;
        }
    }
}