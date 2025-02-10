using TvMaze.Domain.Entities;

namespace TvMaze.Application.Interfaces.Shows
{
    public interface IShowService
    {
        public Task<bool> AddShowIfNotExistsAsync(Show show, List<string> showGenre, CancellationToken cancellationToken);
        Task<bool> UpdateShowAsync(Show show, List<string> showGenere, CancellationToken cancellationToken);
        Task<bool> DeleteShowAsync(int showId, CancellationToken cancellationToken);
        Task<Show?> GetShowByIdAsync(int showId, CancellationToken cancellationToken);
        Task<List<Show>> GetShowListAsync(int pageSize, int pageNo, CancellationToken cancellationToken);
        Task<List<Show>> GetShowListsByNameAync(string name, int pageSize, int pageNo, CancellationToken cancellationToken);
    }
}
