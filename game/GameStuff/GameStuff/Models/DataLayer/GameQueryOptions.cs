using System.Linq;

namespace GameStuff.Models
{
    public class GameQueryOptions : QueryOptions<Game>
    {
        public void SortFilter(GamesGridBuilder builder)
        {
            if (builder.IsFilterByGenre) {
                Where = b => b.GenreId == builder.CurrentRoute.GenreFilter;
            }
            if (builder.IsFilterByPrice) {
                if (builder.CurrentRoute.PriceFilter == "0-20")
                    Where = b => b.Price <= 20;
                else if (builder.CurrentRoute.PriceFilter == "20-70")
                    Where = b => b.Price >= 20 && b.Price <= 70;
                else
                    Where = b => b.Price > 71;
            }
            if (builder.IsFilterByDeveloper) {
                int id = builder.CurrentRoute.DeveloperFilter.ToInt();
                if (id > 0)
                    Where = b => b.GameDevelopers.Any(ba => ba.Developer.DeveloperId == id);
            }

            if (builder.IsSortByGenre) {
                OrderBy = b => b.Genre.Name;
            }
            else if (builder.IsSortByPrice) {
                OrderBy = b => b.Price;
            }
            else  {
                OrderBy = b => b.Title;
            }
        }
    }
}
