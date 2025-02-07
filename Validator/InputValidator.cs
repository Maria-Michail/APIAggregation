namespace AgileActorsApp.Validator
{
    public static class InputValidator
    {
        private static readonly DateTime MinDate = new(1979, 1, 2);
        private static readonly DateTime MaxDate = DateTime.UtcNow;

        private static readonly HashSet<string> AllowedCountries = new()
    {
        "ae", "ar", "at", "au", "be", "bg", "br", "ca", "ch", "cn", "co", "cu", "cz", "de", "eg", "fr", "gb",
        "gr", "hk", "hu", "id", "ie", "il", "in", "it", "jp", "kr", "lt", "lv", "ma", "mx", "my", "ng", "nl",
        "no", "nz", "ph", "pl", "pt", "ro", "rs", "ru", "sa", "se", "sg", "sk", "th", "tr", "tw", "ua", "us",
        "ve", "za"
    };

        private static readonly HashSet<string> AllowedCategories = new()
    {
        "business", "entertainment", "general", "health", "science", "sports", "technology"
    };

        public static string? ValidateCountry(string? country)
        {
            if (string.IsNullOrEmpty(country)) return null;

            country = country.ToLower();
            if (!AllowedCountries.Contains(country))
            {
                throw new ValidationException($"Invalid country code: {country}. Allowed values are: {string.Join(", ", AllowedCountries)}.");
            }

            return country;
        }

        public static string? ValidateCategory(string? category)
        {
            if (string.IsNullOrEmpty(category)) return null;

            category = category.ToLower();
            if (!AllowedCategories.Contains(category))
            {
                throw new ValidationException($"Invalid category: {category}. Allowed values are: {string.Join(", ", AllowedCategories)}.");
            }

            return category;
        }

        public static DateTime ValidateDate(DateTime? date)
        {
            if (date == DateTime.MinValue) return DateTime.Today;

            if (date < MinDate || date > MaxDate)
            {
                throw new ValidationException($"Invalid date. Date must be between {MinDate:yyyy-MM-dd} and {MaxDate:yyyy-MM-dd}.");
            }
            return date.Value;
        }
    }
}
