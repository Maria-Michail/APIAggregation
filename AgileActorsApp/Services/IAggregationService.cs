using AgileActorsApp.Models;

namespace AgileActorsApp.Services
{
    public interface IAggregationService
    {
        Task<AggregationResponse> GetAggregatedDataAsync(AggregationRequest request, CancellationToken cancellationToken);
    }
}
