using AgileActorsApp.ApiKey;
using Microsoft.Extensions.Options;
using System.Net.TMDb;

namespace AgileActorsApp.Services.Movies
{
    public class MoviesService: IMoviesService
    {
        private readonly ApiKeys _apiKeys;
        public MoviesService(IOptions<ApiKeys> apiKeys)
        {
            _apiKeys = apiKeys.Value;
        }

        public async Task<System.Net.TMDb.Movies> GetTopRatedMovies(string language, CancellationToken cancellationToken)
        {
            string apiKey = _apiKeys.MoviesApiKey;
            using (var client = new ServiceClient(apiKey))
            {
                return await client.Movies.GetTopRatedAsync(language, 1, cancellationToken);
            }
        }
    }
}
