using System.Diagnostics;

namespace AgileActorsApp.Middleware
{
    public class RequestTimingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestTimingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            await _next(context);
            stopwatch.Stop();

            var elapsedMs = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Request took {elapsedMs} ms");
        }
    }
}
