using Microsoft.EntityFrameworkCore;
using TvMaze.Application.Interfaces.Shows;
using TvMaze.Domain.Entities;
using TvMaze.Infrastructure.Persistence.Repositories.Generics;

namespace TvMaze.Infrastructure.Persistence.Repositories
{
    public class ShowRepository : GenericRepository<Show>, IShowRepository
    {
        private readonly TvMazeDbContext _context;

        public ShowRepository(TvMazeDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Show>> ShowFilterByNameAsync(string name, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await _context.Shows
                .Where(s => s.Name.ToLower().Contains(name.ToLower()))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Show>> ShowPageAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await _context.Shows
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

    }
}
