namespace AgileActorsApp.Services.Mapping
{
    public static class CountryCoordinatesMapping
    {
        private static readonly Dictionary<string, (double Latitude, double Longitude)> CountryLatLonMap = new()
    {
        { "us", (38.8977, -77.0365) }, // USA - Washington D.C.
        { "gb", (51.5074, -0.1278) },  // UK - London
        { "fr", (48.8566, 2.3522) },   // France - Paris
        { "de", (52.5200, 13.4050) },  // Germany - Berlin
        { "ca", (45.4215, -75.6972) }, // Canada - Ottawa
        { "jp", (35.682839, 139.759455) }, // Japan - Tokyo
        { "au", (-35.2809, 149.1300) }, // Australia - Canberra
        { "in", (28.6139, 77.2090) },  // India - New Delhi
        { "br", (-15.8267, -47.9218) }, // Brazil - Brasília
        { "ru", (55.7558, 37.6173) },  // Russia - Moscow
        { "cn", (39.9042, 116.4074) }, // China - Beijing
        { "za", (-25.7461, 28.1881) }, // South Africa - Pretoria
        { "mx", (19.4326, -99.1332) }, // Mexico - Mexico City
        { "it", (41.9028, 12.4964) },  // Italy - Rome
        { "es", (40.4168, -3.7038) },  // Spain - Madrid
        { "ae", (24.4667, 54.3667) },  // UAE - Abu Dhabi
    };

        public static (double Latitude, double Longitude) GetCoordinates(string? countryCode)
        {
            if (countryCode != null) 
            {
                countryCode = countryCode.ToLower();

                if (CountryLatLonMap.TryGetValue(countryCode, out var coordinates))
                {
                    return coordinates;
                }
            }

            // Default to the US (Washington D.C.) if the country is not found
            return (38.8977, -77.0365);
        }
    }
}
