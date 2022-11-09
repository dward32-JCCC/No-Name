using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStuff.Models
{
    internal class SeedDevelopers : IEntityTypeConfiguration<Developer>
    {
        public void Configure(EntityTypeBuilder<Developer> entity)
        {
            entity.HasData(
                new Developer { DeveloperId = 1, DevName = "InfinityWard" },
                new Developer { DeveloperId = 2, DevName = "Creative Assembly" },
                new Developer { DeveloperId = 3, DevName = "Santa Monica Studio" },
                new Developer { DeveloperId = 4, DevName = "WB Games Montréal" },
                new Developer { DeveloperId = 5, DevName = "Electronic Arts" }

            );
        }
    }

}