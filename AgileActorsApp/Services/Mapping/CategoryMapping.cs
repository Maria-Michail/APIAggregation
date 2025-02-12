using NewsAPI.Constants;

namespace AgileActorsApp.Services.Mapping
{
    public static class CategoryMapping
    {
        public static readonly Dictionary<string, Categories> CategoryMap = new()
    {
        { "business", Categories.Business },
        { "entertainment", Categories.Entertainment },
        { "health", Categories.Health },
        { "science", Categories.Science },
        { "sports", Categories.Sports },
        { "technology", Categories.Technology }
    };

        public static Categories? GetCategoryEnum(string? category)
        {
            if (string.IsNullOrEmpty(category)) return null;
            return CategoryMap.TryGetValue(category.ToLower(), out var categoryEnum) ? categoryEnum : null;
        }
    }

}
