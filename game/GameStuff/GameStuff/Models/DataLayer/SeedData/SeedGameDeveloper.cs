using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStuff.Models
{
    internal class SeedGameDeveloper : IEntityTypeConfiguration<GameDeveloper>
    {
        public void Configure(EntityTypeBuilder<GameDeveloper> entity)
        {
            entity.HasData(
                new GameDeveloper { GameId = 1, DeveloperId = 1 },
                new GameDeveloper { GameId = 2, DeveloperId = 2 },
                new GameDeveloper { GameId = 3, DeveloperId = 3 },
                new GameDeveloper { GameId = 4, DeveloperId = 5 },
                new GameDeveloper { GameId = 5, DeveloperId = 1 }

            );
        }
    }

}
