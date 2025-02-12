namespace AgileActorsApp.Services.Mapping
{
    public static class CountryLanguageMapping
    {
        private static readonly Dictionary<string, string> CountryToLanguageMap = new()
    {
        { "us", "en" }, { "gb", "en" }, { "ca", "en" }, { "fr", "fr" }, { "de", "de" },
        { "es", "es" }, { "it", "it" }, { "jp", "ja" }, { "cn", "zh" }, { "ru", "ru" },
        { "br", "pt" }, { "mx", "es" }, { "ar", "es" }, { "in", "hi" }, { "sa", "ar" },
        { "ae", "ar" }, { "nl", "nl" }, { "se", "sv" }, { "no", "no" }, { "dk", "da" }
    };

        public static string GetLanguage(string? countryCode)
        {
            return CountryToLanguageMap.TryGetValue(countryCode?.ToLower() ?? "", out var language) ? language : "en";
        }
    }
}
