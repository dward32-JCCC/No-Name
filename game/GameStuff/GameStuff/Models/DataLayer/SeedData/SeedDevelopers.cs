using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStuff.Models
{
    internal class SeedDevelopers : IEntityTypeConfiguration<Developer>
    {
        public void Configure(EntityTypeBuilder<Developer> entity)
        {
            entity.HasData(
                new Developer { DeveloperId = 1, FirstName = "Infinity", LastName = "Ward" },
                new Developer { DeveloperId = 2, FirstName = "Creative", LastName = "Assembly" },
                new Developer { DeveloperId = 3, FirstName = "dev3", LastName = "Developer3" },
                new Developer { DeveloperId = 4, FirstName = "dev4", LastName = "Developer4" }

            );
        }
    }

}