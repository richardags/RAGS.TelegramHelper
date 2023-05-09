using System;

namespace TelegramHelper
{
    public class TelegramMessage
    {
        public static string NAME_ICON_INFO = "";

        internal static string ChoiseAOptionAllLanguages()
        {
            return "🇺🇸 <b>Choise a option:</b>\n" +
                "🇧🇷 <b>Escolha uma opção:</b>\n" +
                "🇻🇪 <b>Elije una opción:</b>";
        }

        public static string ChoiceAOption(TelegramLanguageEnum language)
        {
            switch (language)
            {
                case TelegramLanguageEnum.ENGLISH:
                    return "<b>Choise a option:</b>";
                case TelegramLanguageEnum.PORTUGUESE:
                    return "<b>Escolha uma opção:</b>";
                case TelegramLanguageEnum.SPANISH:
                    return "<b>Elije una opción:</b>";
                default:
                    throw new Exception("TelegramMessage.cs ChoiceAOption() - unknown langauge: " + language);
            }            
        }
        public static string LanguageUpdated(TelegramLanguageEnum language)
        {
            switch (language)
            {
                case TelegramLanguageEnum.ENGLISH:
                    return NAME_ICON_INFO + "<b>Language updated.</b>";
                case TelegramLanguageEnum.PORTUGUESE:
                    return NAME_ICON_INFO + "<b>Idioma atualizado.</b>";
                case TelegramLanguageEnum.SPANISH:
                    return NAME_ICON_INFO + "<b>Idioma actualizado.</b>";
                default:
                    throw new Exception("TelegramMessage.cs LanguageUpdated() - unknown langauge: " + language);
            }            
        }
    }
}