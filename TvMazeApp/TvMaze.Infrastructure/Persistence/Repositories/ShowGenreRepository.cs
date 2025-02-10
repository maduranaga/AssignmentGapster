using Microsoft.EntityFrameworkCore;
using TvMaze.Application.Interfaces.ShowGenres;
using TvMaze.Domain.Entities;
using TvMaze.Infrastructure.Persistence.Repositories.Generics;

namespace TvMaze.Infrastructure.Persistence.Repositories
{
    public class ShowGenreRepository : GenericRepository<ShowGenre>, IShowGenreRepository
    {
        private readonly TvMazeDbContext _context;

        public ShowGenreRepository(TvMazeDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteByShowIdAsync(int showId, CancellationToken cancellationToken)
        {
            var deleteRow = await _context.ShowGenres
                .Where(s => s.ShowId == showId)
                .AsNoTracking()
                .ExecuteDeleteAsync(cancellationToken);

            return deleteRow > 0;
        }
    }
}
