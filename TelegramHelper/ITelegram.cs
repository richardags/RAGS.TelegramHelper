using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramHelper
{
    public delegate Task EventOnSendTextMessageAsyncEvent(long chatId, string text, ParseMode parseMode, IReplyMarkup replyMarkup = null);

    public class ITelegram
    {
        public event EventOnSendTextMessageAsyncEvent OnSendTextMessageAsyncEvent;
    }
}