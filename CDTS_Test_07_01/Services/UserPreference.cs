namespace CDN_Application.Services
{
    public class UserPreference
    {
        public string LanguagePreference { get; set; }

        public string Culture { get; set; }

        public int CurrentPageIndex { get; set; } = 0;
    }
}
