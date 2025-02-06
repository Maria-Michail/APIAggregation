namespace AgileActorsApp.Services.Movies
{
    public interface IMoviesService
    {
        Task<System.Net.TMDb.Movies> GetTopRatedMovies(string language, CancellationToken cancellationToken);
    }
}
