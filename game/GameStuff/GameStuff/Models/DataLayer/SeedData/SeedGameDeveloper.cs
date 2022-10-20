using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStuff.Models
{
    internal class SeedGameDeveloper : IEntityTypeConfiguration<GameDeveloper>
    {
        public void Configure(EntityTypeBuilder<GameDeveloper> entity)
        {
            entity.HasData(
                new GameDeveloper { GameId = 1, DeveloperId = 18 },
                new GameDeveloper { GameId = 2, DeveloperId = 20 },
                new GameDeveloper { GameId = 3, DeveloperId = 7 },
                new GameDeveloper { GameId = 4, DeveloperId = 2 },
                new GameDeveloper { GameId = 5, DeveloperId = 19 },
                new GameDeveloper { GameId = 6, DeveloperId = 8 },
                new GameDeveloper { GameId = 7, DeveloperId = 12 },
                new GameDeveloper { GameId = 8, DeveloperId = 16 },
                new GameDeveloper { GameId = 9, DeveloperId = 2 },
                new GameDeveloper { GameId = 10, DeveloperId = 20 },
                new GameDeveloper { GameId = 11, DeveloperId = 15 },
                new GameDeveloper { GameId = 12, DeveloperId = 4 },
                new GameDeveloper { GameId = 13, DeveloperId = 21 },
                new GameDeveloper { GameId = 14, DeveloperId = 5 },
                new GameDeveloper { GameId = 15, DeveloperId = 9 },
                new GameDeveloper { GameId = 16, DeveloperId = 13 },
                new GameDeveloper { GameId = 17, DeveloperId = 7 },
                new GameDeveloper { GameId = 18, DeveloperId = 4 },
                new GameDeveloper { GameId = 19, DeveloperId = 11 },
                new GameDeveloper { GameId = 20, DeveloperId = 22 },
                new GameDeveloper { GameId = 21, DeveloperId = 17 },
                new GameDeveloper { GameId = 22, DeveloperId = 3 },
                new GameDeveloper { GameId = 23, DeveloperId = 14 },
                new GameDeveloper { GameId = 24, DeveloperId = 1 },
                new GameDeveloper { GameId = 25, DeveloperId = 10 },
                new GameDeveloper { GameId = 26, DeveloperId = 6 },
                new GameDeveloper { GameId = 27, DeveloperId = 23 },
                new GameDeveloper { GameId = 28, DeveloperId = 4 },
                new GameDeveloper { GameId = 28, DeveloperId = 26 },
                new GameDeveloper { GameId = 29, DeveloperId = 25 }
            );
        }
    }

}
