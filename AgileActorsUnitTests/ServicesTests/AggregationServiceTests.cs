using Moq;
using AgileActorsApp.Services;
using AgileActorsApp.Models;
using AgileActorsApp.Services.Movies;
using AgileActorsApp.Services.News;
using AgileActorsApp.Services.Weather;
using NewsAPI.Models;

namespace AgileActorsUnitTests.ServicesTests
{ 
    [TestClass]
    public class AggregationServiceTests
    {
        private Mock<INewsService> _mockNewsService;
        private Mock<IWeatherService> _mockWeatherService;
        private Mock<IMoviesService> _mockMoviesService;
        private AggregationService _aggregationService;

        [TestInitialize]
        public void Setup()
        {
            _mockNewsService = new Mock<INewsService>();
            _mockWeatherService = new Mock<IWeatherService>();
            _mockMoviesService = new Mock<IMoviesService>();

            _aggregationService = new AggregationService(
                _mockNewsService.Object,
                _mockWeatherService.Object,
                _mockMoviesService.Object
            );
        }

        [TestMethod]
        public async Task GetAggregatedDataAsync_ValidRequest_ReturnsAggregatedData()
        {
            // Arrange
            var request = new AggregationRequest
            {
                Country = "US",
                NewsCategory = "business",
                Date = DateTime.Today
            };

            SetupMockServices(request);

            // Act
            var result = await _aggregationService.GetAggregatedDataAsync(request, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Articles.Articles.Count);
            Assert.AreEqual("Movie 1", result.Movies.Results.First().Title);
            Assert.AreEqual(20, result.Weather.Temperature);
        }

        [TestMethod]
        public async Task GetAggregatedDataAsync_SortByDate_SortsNewsAndMovies()
        {
            // Arrange
            var request = new AggregationRequest
            {
                Country = "US",
                NewsCategory = "business",
                Date = DateTime.Today,
                SortBy = SortByOption.Date
            };

            SetupMockServices(request);

            // Act
            var result = await _aggregationService.GetAggregatedDataAsync(request, CancellationToken.None);

            // Assert
            Assert.AreEqual("News Article 1", result.Articles.Articles.First().Title);
            Assert.AreEqual("Movie 1", result.Movies.Results.First().Title);
        }

        [TestMethod]
        public async Task GetAggregatedDataAsync_SortByPopularity_SortsMovies()
        {
            // Arrange
            var request = new AggregationRequest
            {
                Country = "US",
                NewsCategory = "business",
                Date = DateTime.Today,
                SortBy = SortByOption.Popularity
            };

            SetupMockServices(request);

            // Act
            var result = await _aggregationService.GetAggregatedDataAsync(request, CancellationToken.None);

            // Assert
            Assert.AreEqual("Movie 2", result.Movies.Results.First().Title);
        }

        [TestMethod]
        public async Task GetAggregatedDataAsync_NoSorting_ReturnsUnsortedData()
        {
            // Arrange
            var request = new AggregationRequest
            {
                Country = "US",
                NewsCategory = "business",
                Date = DateTime.Today
            };

            SetupMockServices(request);

            // Act
            var result = await _aggregationService.GetAggregatedDataAsync(request, CancellationToken.None);

            // Assert
            Assert.AreEqual("News Article 2", result.Articles.Articles.Last().Title);
            Assert.AreEqual("Movie 2", result.Movies.Results.Last().Title);
        }

        private void SetupMockServices(AggregationRequest request)
        {
            var mockNewsArticles = new ArticlesResult
            {
                Articles = new List<NewsAPI.Models.Article>
            {
                new NewsAPI.Models.Article { Title = "News Article 1", PublishedAt = DateTime.Today },
                new NewsAPI.Models.Article { Title = "News Article 2", PublishedAt = DateTime.Today.AddDays(-1) }
            }
            };

            var mockWeather = new WeatherResponse
            {
                Temperature = 20,
                Humidity = 80,
                WindSpeed = 10,
                Description = "Sunny"
            };

            var mockMovies = new MovieSearchResponse
            {
                Results = new List<MovieResponse>
            {
                new MovieResponse { Title = "Movie 1", ReleaseDate = DateTime.Parse("2025-01-01"), Popularity = 50 },
                new MovieResponse { Title = "Movie 2", ReleaseDate = DateTime.Parse("2025-01-01"), Popularity = 100 }
            }
            };

            _mockNewsService.Setup(s => s.GetTopHeadlinesAsync(It.IsAny<NewsAPI.Constants.Countries>(), It.IsAny<NewsAPI.Constants.Categories>(), It.IsAny<string>()))
                .ReturnsAsync(mockNewsArticles);

            _mockWeatherService.Setup(s => s.GetWeatherAsync(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<DateTime>()))
                .ReturnsAsync(mockWeather);

            _mockMoviesService.Setup(s => s.GetTopRatedMovies(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockMovies);
        }
    }
}
