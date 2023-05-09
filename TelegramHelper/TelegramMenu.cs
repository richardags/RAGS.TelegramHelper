using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramHelper
{
    public class TelegramMenu
    {
        public class Button
        {
            public string text;
            public string callbackData;
            public bool isURL = false;

            public Button(string text, string callbackData)
            {
                this.text = text;
                this.callbackData = callbackData;
            }
            public Button(string text, string callbackData, bool isURL)
            {
                this.text = text;
                this.callbackData = callbackData;
                this.isURL = isURL;
            }
        }

        internal static InlineKeyboardMarkup DefaultMenu()
        {
            return GetMenuHorizontally(new List<Button>() {
                new Button(TelegramButton.GetString(
                    CommandType.ENGLISH
                ), CommandType.ENGLISH.ToString()),
                new Button(TelegramButton.GetString(
                    CommandType.PORTUGUESE
                ), CommandType.PORTUGUESE.ToString()),
                new Button(TelegramButton.GetString(
                    CommandType.SPANISH
                ), CommandType.SPANISH.ToString())
            });
        }

        public static Button ChangeLanguageButton(TelegramLanguageEnum language)
        {
            return new Button(
                TelegramButton.GetString(language, CommandType.CHANGE_LANGUAGE),
                CommandType.CHANGE_LANGUAGE.ToString());
        }
        public static Button LogoutButton(TelegramLanguageEnum language)
        {
            return new Button(
                TelegramButton.GetString(language, CommandType.LOGOUT),
                CommandType.LOGOUT.ToString());
        }
        public static Button CancelAnswerButton(TelegramLanguageEnum language)
        {
            return new Button(
                TelegramButton.GetString(language, CommandType.CANCEL_ANSWER),
                CommandType.CANCEL_ANSWER.ToString());
        }

        public static InlineKeyboardMarkup GetMenuVertically(List<Button> buttons)
        {
            List<List<InlineKeyboardButton>> inlineKeyboardButtonsArray =
                new List<List<InlineKeyboardButton>>();

            foreach (Button button in buttons)
            {
                if (button.isURL)
                {
                    inlineKeyboardButtonsArray.Add(new List<InlineKeyboardButton>() {
                        InlineKeyboardButton.WithUrl(button.text, button.callbackData)
                    });
                }
                else
                {
                    inlineKeyboardButtonsArray.Add(new List<InlineKeyboardButton>() {
                        InlineKeyboardButton.WithCallbackData(button.text, button.callbackData)
                    });
                }
            }

            return new InlineKeyboardMarkup(inlineKeyboardButtonsArray);
        }
        public static InlineKeyboardMarkup GetMenuHorizontally(List<Button> buttons)
        {
            List<InlineKeyboardButton> inlineKeyboardButtonsArray =
                new List<InlineKeyboardButton>();

            foreach (Button button in buttons)
            {
                if (button.isURL)
                {
                    inlineKeyboardButtonsArray.Add(InlineKeyboardButton.WithUrl(
                        button.text,
                        button.callbackData));
                }
                else
                {
                    inlineKeyboardButtonsArray.Add(InlineKeyboardButton.WithCallbackData(
                        button.text,
                        button.callbackData));
                }
            }

            return new InlineKeyboardMarkup(inlineKeyboardButtonsArray);
        }
    }
}