using NewsAPI.Models;

namespace AgileActorsApp.Models
{
    public class AggregationResponse
    {
        public ArticlesResult Articles { get; set; }
        public WeatherResponse Weather { get; set; }
        public MovieSearchResponse Movies { get; set; }
    }
}
