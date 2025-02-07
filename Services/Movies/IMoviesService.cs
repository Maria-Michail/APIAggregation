using AgileActorsApp.Models;

namespace AgileActorsApp.Services.Movies
{
    public interface IMoviesService
    {
        Task<MovieSearchResponse> GetTopRatedMovies(string language, CancellationToken cancellationToken);
    }
}
