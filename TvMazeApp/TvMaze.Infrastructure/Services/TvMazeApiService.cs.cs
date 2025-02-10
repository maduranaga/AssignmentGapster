using Newtonsoft.Json.Linq;
using TvMaze.Application.DTOs;
using TvMaze.Application.Interfaces.TvMaze;

namespace TvMaze.Infrastructure.Services
{
    public class TvMazeApiService : ITvMazeApiService
    {
        private readonly HttpClient _httpClient;

        public TvMazeApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<TVMazeShow>> GetShowByNameAsync(string showName)
        {
            if (string.IsNullOrWhiteSpace(showName))
            {
                return new List<TVMazeShow>();
            }

            try
            {
                var response = await _httpClient.GetAsync($"search/shows?q={Uri.EscapeDataString(showName)}");

                if (!response.IsSuccessStatusCode)
                {
                    return new List<TVMazeShow>();
                }

                var json = await response.Content.ReadAsStringAsync();
                var jsonArray = JArray.Parse(json);

                return jsonArray
                    .Select(item => item["show"]?.ToObject<TVMazeShow>())
                    .Where(show => show != null)
                    .ToList() ?? new List<TVMazeShow>();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}