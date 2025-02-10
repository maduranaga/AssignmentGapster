using TvMaze.Application.Interfaces.Generic;
using TvMaze.Domain.Entities;

namespace TvMaze.Application.Interfaces.Shows
{
    public interface IShowRepository : IGenericRepository<Show>
    {
        public Task<List<Show>> ShowFilterByNameAsync(string Name, int pageNumber, int pageSize, CancellationToken cancellationToken);
        public Task<List<Show>> ShowPageAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
