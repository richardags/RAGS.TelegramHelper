using System;

namespace TelegramHelper
{
    public class TelegramButton
    {
        internal static string GetString(CommandType commandType)
        {
            return GetString(TelegramLanguageEnum.ENGLISH, commandType);
        }

        internal static string GetString(TelegramLanguageEnum language,
            CommandType commandType)
        {
            switch (commandType)
            {
                case CommandType.ENGLISH:
                    return "🇺🇸 English";
                case CommandType.PORTUGUESE:
                    return "🇧🇷 Português";
                case CommandType.SPANISH:
                    return "🇻🇪 Español";
                case CommandType.CHANGE_LANGUAGE:
                    {
                        switch (language)
                        {
                            case TelegramLanguageEnum.ENGLISH:
                                return "🧩 Change language";
                            case TelegramLanguageEnum.PORTUGUESE:
                                return "🧩 Trocar o idioma";
                            case TelegramLanguageEnum.SPANISH:
                                return "🧩 Cambiar el idioma";
                        }
                    }
                    break;
                case CommandType.LOGOUT:
                    {
                        switch (language)
                        {
                            case TelegramLanguageEnum.ENGLISH:
                                return "🗝 logout";
                            case TelegramLanguageEnum.PORTUGUESE:
                                return "🗝 deslogar";
                            case TelegramLanguageEnum.SPANISH:
                                return "🗝 cerrar sesión";
                        }
                    }
                    break;
                case CommandType.CANCEL_ANSWER:
                    {
                        switch (language)
                        {
                            case TelegramLanguageEnum.ENGLISH:
                                return "❎ cancel answer";
                            case TelegramLanguageEnum.PORTUGUESE:
                                return "❎ cancelar resposta";
                            case TelegramLanguageEnum.SPANISH:
                                return " ❎cancelar respuesta";
                        }
                    }
                    break;
            }

            throw new Exception("TelegramButton.class GetString() - unknown commandType: " + commandType);
        }
    }
}