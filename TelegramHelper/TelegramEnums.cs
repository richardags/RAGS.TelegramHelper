namespace TelegramHelper
{
    public enum TelegramLanguageEnum
    {
        NONE,

        ENGLISH,
        PORTUGUESE,
        SPANISH
    }

    internal enum CommandType
    {
        NONE,

        ENGLISH,
        PORTUGUESE,
        SPANISH,

        CHANGE_LANGUAGE,
        LOGOUT,
        CANCEL_ANSWER
    }
}