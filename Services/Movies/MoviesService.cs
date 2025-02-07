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
        private readonly MemoryCacheService _cacheService;

        public MoviesService(IOptions<ApiKeys> apiKeys, MemoryCacheService cacheService)
        {
            _apiKeys = apiKeys.Value;
            _cacheService = cacheService;
        }

        public async Task<MovieSearchResponse> GetTopRatedMovies(string language, CancellationToken cancellationToken)
        {
            string cacheKey = $"movies_toprated_{language}";

            return await _cacheService.GetOrSetAsync(cacheKey, async () =>
            {
                string apiKey = _apiKeys.MoviesApiKey;
                using (var client = new ServiceClient(apiKey))
                {
                    var movies = await client.Movies.GetTopRatedAsync(language, 1, cancellationToken);

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

                    return mappedMovies;
                }
            });
        }
    }
}
