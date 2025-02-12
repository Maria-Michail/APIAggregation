using NewsAPI.Constants;
using NewsAPI.Models;

namespace AgileActorsApp.Services.News
{
    public interface INewsService
    {
        Task<ArticlesResult> GetTopHeadlinesAsync(Countries? country, Categories? category, string? q);
    }
}
