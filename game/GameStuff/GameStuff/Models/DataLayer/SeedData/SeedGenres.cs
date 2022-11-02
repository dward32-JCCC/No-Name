using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStuff.Models
{
    internal class SeedGenres : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> entity)
        {
            entity.HasData(
                new Genre { GenreId = "FPS", Name = "FPS" },
                new Genre { GenreId = "RTS", Name = "RTS" },
                new Genre { GenreId = "MMO", Name = "MMO" }

            );
        }
    }

}
