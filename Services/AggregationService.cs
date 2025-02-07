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
            request.Country = InputValidator.ValidateCountry(request.Country);
            request.NewsCategory = InputValidator.ValidateCategory(request.NewsCategory);
            request.Date = InputValidator.ValidateDate(request.Date);

            var countryEnum = CountryMapping.GetCountryEnum(request.Country);
            var categoryEnum = CategoryMapping.GetCategoryEnum(request.NewsCategory);
            var coordinates = CountryCoordinatesMapping.GetCoordinates(request.Country);
            var language = CountryLanguageMapping.GetLanguage(request.Country);

            var newsTask = _newsService.GetTopHeadlinesAsync(countryEnum, categoryEnum, request.NewsKeyword);
            var weatherTask = _weatherService.GetWeatherAsync(coordinates.Latitude, coordinates.Longitude, request.Date);
            var moviesTask = _moviesService.GetTopRatedMovies(language, cancellationToken);

            await Task.WhenAll(newsTask, weatherTask, moviesTask);

            var newsArticles = newsTask.Result;
            var weather = weatherTask.Result;
            var movies = moviesTask.Result;

            ApplySorting(ref newsArticles, ref movies, request.SortBy);

            return new AggregationResponse
            {
                Articles = newsArticles,
                Weather = weather,
                Movies = movies,
            };
        }

        private void ApplySorting(ref ArticlesResult newsArticles, ref MovieSearchResponse movies, SortByOption? sortBy)
        {
            if (sortBy == null) return;

            switch (sortBy)
            {
                case SortByOption.Date:
                    newsArticles.Articles = newsArticles.Articles?.OrderByDescending(a => a.PublishedAt).ToList();
                    movies.Results = movies.Results?.OrderByDescending(m => m.ReleaseDate).ToList();
                    break;
                case SortByOption.Popularity:
                    movies.Results = movies.Results?.OrderByDescending(m => m.Popularity).ToList();
                    break;
            }
        }

    }
}
