using TvMaze.Application.DTOs;

namespace TvMaze.Application.Interfaces.TvMaze
{
    public interface ITvMazeApiService
    {
        public Task<List<TVMazeShow>> GetShowByNameAsync(string name);
    }
}
