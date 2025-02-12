using AgileActorsApp.ApiKey;
using AgileActorsApp.Cache;
using AgileActorsApp.Models;
using Microsoft.Extensions.Options;
using System.Net.TMDb;

namespace AgileActorsApp.Services.Movies
{
    public class MoviesService: IMoviesService
    {
        private readonly ApiKeys _apiKeys;
        private readonly IMemoryCacheService _cacheService;
        private readonly ILogger<MoviesService> _logger;

        public MoviesService(IOptions<ApiKeys> apiKeys, IMemoryCacheService cacheService, ILogger<MoviesService> logger)
        {
            _apiKeys = apiKeys.Value;
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task<MovieSearchResponse> GetTopRatedMovies(string language, CancellationToken cancellationToken)
        {
            string cacheKey = $"movies_toprated_{language}";

            var movieSearchResponse = await _cacheService.GetOrSetAsync(cacheKey, async () =>
            {
                string apiKey = _apiKeys.MoviesApiKey;
                using (var client = new ServiceClient(apiKey))
                {
                    try
                    {
                        var movies = await client.Movies.GetTopRatedAsync(language, 1, cancellationToken);

                        if (movies == null || movies.Results == null || !movies.Results.Any())
                        {
                            _logger.LogWarning("No top-rated movies found for language: {Language}.", language);
                        }

                        var mappedMovies = new MovieSearchResponse
                        {
                            TotalResults = movies.TotalCount,
                            Results = movies.Results?.Select(m => new MovieResponse
                            {
                                Adult = m.Adult,
                                Overview = m.Overview,
                                ReleaseDate = m.ReleaseDate,
                                Id = m.Id,
                                OriginalTitle = m.OriginalTitle,
                                Title = m.Title,
                                Popularity = (double)m.Popularity,
                                VoteCount = m.VoteCount,
                                VoteAverage = (double)m.VoteAverage
                            }).ToList() ?? new List<MovieResponse>()
                        };

                        _logger.LogInformation("Successfully fetched top-rated movies for language: {Language}. Total results: {TotalResults}", language, mappedMovies.TotalResults);

                        return mappedMovies;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "An error occurred while fetching top-rated movies data for language: {Language}", language);
                        return new MovieSearchResponse();
                    }
                }
            });
            return movieSearchResponse ?? new MovieSearchResponse();
        }
    }
}
