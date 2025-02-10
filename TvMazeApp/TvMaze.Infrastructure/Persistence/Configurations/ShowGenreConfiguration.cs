using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TvMaze.Domain.Entities;

namespace TvMaze.Infrastructure.Persistence.Configurations
{
    public class ShowGenreConfiguration : IEntityTypeConfiguration<ShowGenre>
    {
        public void Configure(EntityTypeBuilder<ShowGenre> builder)
        {
            builder.ToTable("ShowGenres");

            builder.HasKey(s => s.Id);

            builder.HasOne(sg => sg.Show)
                   .WithMany(s => s.ShowGenres)
                   .HasForeignKey(sg => sg.ShowId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(sg => sg.Genre)
                   .IsRequired()
                   .HasMaxLength(100);
        }
    }
}