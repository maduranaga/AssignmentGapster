using Microsoft.Extensions.Caching.Memory;
using TvMaze.Application.Interfaces.ShowGenres;
using TvMaze.Application.Interfaces.Shows;
using TvMaze.Application.Interfaces.TvMaze;
using TvMaze.Application.Interfaces.UniOfWork;
using TvMaze.Application.Services.Shows;
using TvMaze.Infrastructure.Persistence.Repositories;
using TvMaze.Infrastructure.Persistence.Repositories.Caches;
using TvMaze.Infrastructure.Persistence.Repositories.UniOfWork;
using TvMaze.Infrastructure.Services;

namespace TvMaze.Api.Extensions
{
    public static class ApplicationServices
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var tvMazeApiBaseUrl = configuration["TvMazeApi:BaseUrl"]??"";

            services.AddHttpClient<ITvMazeApiService, TvMazeApiService>(client =>
            {
                client.BaseAddress = new Uri(tvMazeApiBaseUrl);
            });

            services.AddMemoryCache();
            services.AddTransient<ShowRepository>();

            services.AddTransient<IShowRepository>(provider =>
            {
                var memoryCache = provider.GetRequiredService<IMemoryCache>();
                var repository = provider.GetRequiredService<ShowRepository>();

                return new CachedShow(repository, memoryCache);
            });

            services.AddTransient<IShowGenreRepository, ShowGenreRepository>();
            services.AddTransient<IShowService, ShowService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
