using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;

namespace TelegramHelper
{
    internal class TelegramSendMenu
    {
        public static async Task DefaultMenuAsync(
            EventOnSendTextMessageAsyncEvent OnSendTextMessageAsyncEvent,
            TelegramChat chat)
        {            
            await OnSendTextMessageAsyncEvent(
                chat.id,
                TelegramMessage.ChoiseAOptionAllLanguages(),
                ParseMode.Html,
                TelegramMenu.DefaultMenu());
        }
    }
}