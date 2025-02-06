namespace AgileActorsApp.Models
{
    public class AggregationRequest
    {
        public string? NewsCountry { get; set; }
        public string? NewsCategory { get; set; }
        public string? NewsKeyword { get; set; }
        public DateTime? Date { get; set; } = DateTime.Today;
    }
}
