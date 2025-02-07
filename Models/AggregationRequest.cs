using System.Text.Json.Serialization;

namespace AgileActorsApp.Models
{
    public enum SortByOption
    {
        Date,
        Popularity
    }

    public class AggregationRequest
    {
        public string? Country { get; set; }
        public string? NewsCategory { get; set; }
        public string? NewsKeyword { get; set; }
        public DateTime Date { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SortByOption? SortBy { get; set; }
    }
}
