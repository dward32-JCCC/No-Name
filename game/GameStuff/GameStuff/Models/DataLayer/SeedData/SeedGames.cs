using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStuff.Models
{
    internal class SeedGames : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> entity)
        {
            entity.HasData(
                new Game { GameId = 1, Title = "Call of Duty: Modern Warfare II", GenreId = "FPS", Price = 69.99 },
                new Game { GameId = 2, Title = "Halo Wars 2", GenreId = "RTS", Price = 59.99 },
                new Game { GameId = 3, Title = "God of War Ragnarok", GenreId = "Action", Price = 69.99 },
                new Game { GameId = 4, Title = "Gotham Knights", GenreId = "Action", Price = 69.99 },
                new Game { GameId = 5, Title = "Madden NFL 22", GenreId = "Sports", Price = 19.99 }
            );
        }
    }

}
