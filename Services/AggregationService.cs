using AgileActorsApp.Models;
using AgileActorsApp.Services.Mapping;
using AgileActorsApp.Services.Movies;
using AgileActorsApp.Services.News;
using AgileActorsApp.Services.Weather;
using AgileActorsApp.Validator;
using NewsAPI.Models;

namespace AgileActorsApp.Services
{
    public class AggregationService: IAggregationService
    {
        private readonly INewsService _newsService;
        private readonly IWeatherService _weatherService;
        private readonly IMoviesService _moviesService;

        public AggregationService(INewsService newsService, IWeatherService weatherService, IMoviesService moviesService)
        {
            _newsService = newsService;
            _weatherService = weatherService;
            _moviesService = moviesService;
        }

        public async Task<AggregationResponse> GetAggregatedDataAsync(AggregationRequest request, CancellationToken cancellationToken)
        {
            request.NewsCountry = InputValidator.ValidateCountry(request.NewsCountry);
            request.NewsCategory = InputValidator.ValidateCategory(request.NewsCategory);
            request.Date = InputValidator.ValidateDate(request.Date);

            var countryEnum = CountryMapping.GetCountryEnum(request.NewsCountry);
            var categoryEnum = CategoryMapping.GetCategoryEnum(request.NewsCategory);
            var coordinates = CountryCoordinatesMapping.GetCoordinates(request.NewsCountry);
            var language = CountryLanguageMapping.GetLanguage(request.NewsCountry);

            var newsTask = _newsService.GetTopHeadlinesAsync(countryEnum, categoryEnum, request.NewsKeyword);
            var weatherTask = _weatherService.GetWeatherAsync(coordinates.Latitude, coordinates.Longitude, request.Date);
            var moviesTask = _moviesService.GetTopRatedMovies(language, cancellationToken);

            await Task.WhenAll(newsTask, weatherTask);

            var newsArticles = newsTask.Result ?? new ArticlesResult();
            var weather = weatherTask.Result ?? new WeatherResponse();
            var movies = moviesTask.Result ?? new System.Net.TMDb.Movies();

            return new AggregationResponse
            {
                Articles = newsArticles,
                Weather = weather,
                Movies = movies,
            };
        }
    }
}
