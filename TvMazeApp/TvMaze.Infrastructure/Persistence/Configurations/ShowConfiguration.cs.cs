using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TvMaze.Domain.Entities;

namespace TvMaze.Infrastructure.Persistence.Configurations
{
    public class ShowConfiguration : IEntityTypeConfiguration<Show>
    {
        public void Configure(EntityTypeBuilder<Show> builder)
        {
            builder.ToTable("Shows");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(s => s.Language)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(s => s.Premiered)
                   .HasColumnType("DATE");

            builder.HasMany(s => s.ShowGenres)
                   .WithOne(sg => sg.Show)
                   .HasForeignKey(sg => sg.ShowId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
