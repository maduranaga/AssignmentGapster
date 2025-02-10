using Microsoft.EntityFrameworkCore;
using TvMaze.Domain.Entities;
using TvMaze.Infrastructure.Persistence.Configurations;

namespace TvMaze.Infrastructure.Persistence
{
    public class TvMazeDbContext : DbContext
    {
        public TvMazeDbContext(DbContextOptions<TvMazeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Show> Shows { get; set; }
        public DbSet<ShowGenre> ShowGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ShowConfiguration());
            modelBuilder.ApplyConfiguration(new ShowGenreConfiguration());
        }

    }
}
