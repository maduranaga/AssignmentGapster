namespace TvMaze.Domain.RateLimit
{
    public class MyRateLimiterOptions
    {
        public int PermitLimit { get; set; }
        public int Window { get; set; }
        public int QueueLimit { get; set; }
    }

}
