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
                new Game { GameId = 3, Title = "game3", GenreId = "MMO", Price = 29.99 },
                new Game { GameId = 4, Title = "game4", GenreId = "RTS", Price = 50.00 },
                new Game { GameId = 5, Title = "game5", GenreId = "FPS", Price = 29.99 }
            );
        }
    }

}
