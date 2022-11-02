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
                if (builder.CurrentRoute.PriceFilter == "under7")
                    Where = b => b.Price < 7;
                else if (builder.CurrentRoute.PriceFilter == "7to14")
                    Where = b => b.Price >= 7 && b.Price <= 14;
                else
                    Where = b => b.Price > 14;
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
