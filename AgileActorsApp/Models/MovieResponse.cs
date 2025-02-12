namespace AgileActorsApp.Models
{
    public class MovieResponse
    {
        public bool Adult { get; set; }
        public string? Overview { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int Id { get; set; }
        public string? OriginalTitle { get; set; }
        public string? Title { get; set; }
        public double Popularity { get; set; }
        public int VoteCount { get; set; }
        public double VoteAverage { get; set; }
    }

    public class MovieSearchResponse
    {
        public List<MovieResponse> Results { get; set; } = new List<MovieResponse>();

        public int TotalResults { get; set; }
     }
}
