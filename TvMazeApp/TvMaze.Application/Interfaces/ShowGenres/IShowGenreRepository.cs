using TvMaze.Application.Interfaces.Generic;
using TvMaze.Domain.Entities;

namespace TvMaze.Application.Interfaces.ShowGenres
{
    public interface IShowGenreRepository : IGenericRepository<ShowGenre>
    {
        public Task<bool> DeleteByShowIdAsync(int showId, CancellationToken cancellationToken);
    }
}
