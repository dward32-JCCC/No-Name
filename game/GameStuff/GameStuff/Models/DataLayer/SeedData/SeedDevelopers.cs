using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStuff.Models
{
    internal class SeedDevelopers : IEntityTypeConfiguration<Developer>
    {
        public void Configure(EntityTypeBuilder<Developer> entity)
        {
            entity.HasData(
                new Developer { DeveloperId = 1, FirstName = "Michelle", LastName = "Alexander" },
                new Developer { DeveloperId = 2, FirstName = "Stephen E.", LastName = "Ambrose" },
                new Developer { DeveloperId = 3, FirstName = "Margaret", LastName = "Atwood" },
                new Developer { DeveloperId = 4, FirstName = "Jane", LastName = "Austen" },
                new Developer { DeveloperId = 5, FirstName = "James", LastName = "Baldwin" },
                new Developer { DeveloperId = 6, FirstName = "Emily", LastName = "Bronte" },
                new Developer { DeveloperId = 7, FirstName = "Agatha", LastName = "Christie" },
                new Developer { DeveloperId = 8, FirstName = "Ta-Nehisi", LastName = "Coates" },
                new Developer { DeveloperId = 9, FirstName = "Jared", LastName = "Diamond" },
                new Developer { DeveloperId = 10, FirstName = "Joan", LastName = "Didion" },
                new Developer { DeveloperId = 11, FirstName = "Daphne", LastName = "Du Maurier" },
                new Developer { DeveloperId = 12, FirstName = "Tina", LastName = "Fey" },
                new Developer { DeveloperId = 13, FirstName = "Roxane", LastName = "Gay" },
                new Developer { DeveloperId = 14, FirstName = "Dashiel", LastName = "Hammett" },
                new Developer { DeveloperId = 15, FirstName = "Frank", LastName = "Herbert" },
                new Developer { DeveloperId = 16, FirstName = "Aldous", LastName = "Huxley" },
                new Developer { DeveloperId = 17, FirstName = "Stieg", LastName = "Larsson" },
                new Developer { DeveloperId = 18, FirstName = "David", LastName = "McCullough" },
                new Developer { DeveloperId = 19, FirstName = "Toni", LastName = "Morrison" },
                new Developer { DeveloperId = 20, FirstName = "George", LastName = "Orwell" },
                new Developer { DeveloperId = 21, FirstName = "Mary", LastName = "Shelley" },
                new Developer { DeveloperId = 22, FirstName = "Sun", LastName = "Tzu" },
                new Developer { DeveloperId = 23, FirstName = "Augusten", LastName = "Burroughs" },
                new Developer { DeveloperId = 25, FirstName = "JK", LastName = "Rowling" },
                new Developer { DeveloperId = 26, FirstName = "Seth", LastName = "Grahame-Smith" }
            );
        }
    }

}