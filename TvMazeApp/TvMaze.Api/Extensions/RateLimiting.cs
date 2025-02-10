using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using TvMaze.Domain.RateLimit;

namespace TvMaze.Api.Extensions
{
    public static class RateLimiting
    {
        public static void AddRateLimiting(this IServiceCollection services, IConfiguration configuration)
        {
            var myOptions = configuration.GetSection("RateLimiterOptions").Get<MyRateLimiterOptions>();

            services.AddRateLimiter(_ => _
                .AddFixedWindowLimiter(policyName: "fixedPolicy", options =>
                {
                    options.PermitLimit = myOptions?.PermitLimit ?? 5;
                    options.Window = TimeSpan.FromSeconds(myOptions?.Window ?? 5);
                    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    options.QueueLimit = myOptions?.QueueLimit ?? 5;
                }));
        }
    }
}
