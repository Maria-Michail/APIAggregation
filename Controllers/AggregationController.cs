using AgileActorsApp.Models;
using AgileActorsApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgileActorsApp.Controllers
{
    [Route("api/aggregate")]
    public class AggregationController : ControllerBase
    {
        private readonly IAggregationService _aggregationService;

        public AggregationController(IAggregationService aggregationService)
        {
            _aggregationService = aggregationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAggregatedData([FromQuery] AggregationRequest request, CancellationToken cancellationToken)
        {
            var data = await _aggregationService.GetAggregatedDataAsync(request, cancellationToken);

            if (data.Articles == null && data.Weather == null)
            {
                return NotFound("No data available from external APIs.");
            }

            return Ok(data);
        }
    }
}
