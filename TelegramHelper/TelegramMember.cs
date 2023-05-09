namespace TelegramHelper
{
    public class TelegramMember
    {
        public bool IsAuthenticated = false;
        public int waintingAnswer = 0;

        public TelegramLanguageEnum language = TelegramLanguageEnum.ENGLISH;
        public TelegramUser user;
        public object data;

        internal bool IsWaintingAnswer()
        {
            return waintingAnswer != 0;
        }

        public void CancelAnswer()
        {
            waintingAnswer = (int)CommandType.NONE;
        }

        //public List<int> menu = new List<int>(); //create a class for this - forward and backward menu

    }
}