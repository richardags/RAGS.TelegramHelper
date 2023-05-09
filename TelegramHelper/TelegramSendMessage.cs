using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;

namespace TelegramHelper
{
    internal class TelegramSendMessage
    {
        public static async Task LanguageUpdatedAsync(EventOnSendTextMessageAsyncEvent OnSendTextMessageAsyncEvent,
            TelegramMember member)
        {
            await OnSendTextMessageAsyncEvent(
                member.user.chatId,
                TelegramMessage.LanguageUpdated(member.language),
                ParseMode.Html);
        }
    }
}