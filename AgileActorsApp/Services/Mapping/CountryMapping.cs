using NewsAPI.Constants;

namespace AgileActorsApp.Services.Mapping
{
    public static class CountryMapping
    {
        public static readonly Dictionary<string, Countries> CountryMap = new()
    {
        { "AE", Countries.AE },
        { "AR", Countries.AR },
        { "AT", Countries.AT },
        { "AU", Countries.AU },
        { "BE", Countries.BE },
        { "BG", Countries.BG },
        { "BR", Countries.BR },
        { "CA", Countries.CA },
        { "CH", Countries.CH },
        { "CN", Countries.CN },
        { "CO", Countries.CO },
        { "CU", Countries.CU },
        { "CZ", Countries.CZ },
        { "DE", Countries.DE },
        { "EG", Countries.EG },
        { "FR", Countries.FR },
        { "GB", Countries.GB },
        { "GR", Countries.GR },
        { "HK", Countries.HK },
        { "HU", Countries.HU },
        { "ID", Countries.ID },
        { "IE", Countries.IE },
        { "IL", Countries.IL },
        { "IN", Countries.IN },
        { "IT", Countries.IT },
        { "JP", Countries.JP },
        { "KR", Countries.KR },
        { "LT", Countries.LT },
        { "LV", Countries.LV },
        { "MA", Countries.MA },
        { "MX", Countries.MX },
        { "MY", Countries.MY },
        { "NG", Countries.NG },
        { "NL", Countries.NL },
        { "NO", Countries.NO },
        { "NZ", Countries.NZ },
        { "PH", Countries.PH },
        { "PL", Countries.PL },
        { "PT", Countries.PT },
        { "RO", Countries.RO },
        { "RS", Countries.RS },
        { "RU", Countries.RU },
        { "SA", Countries.SA },
        { "SE", Countries.SE },
        { "SG", Countries.SG },
        { "SI", Countries.SI },
        { "SK", Countries.SK },
        { "TH", Countries.TH },
        { "TR", Countries.TR },
        { "TW", Countries.TW },
        { "UA", Countries.UA },
        { "US", Countries.US },
        { "VE", Countries.VE },
        { "ZA", Countries.ZA }
    };

        public static Countries? GetCountryEnum(string? countryCode)
        {
            if (string.IsNullOrEmpty(countryCode)) return null;
            return CountryMap.TryGetValue(countryCode.ToUpper(), out var countryEnum) ? countryEnum : null;
        }
    }
}
