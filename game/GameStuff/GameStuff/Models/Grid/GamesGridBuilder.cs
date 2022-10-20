using Microsoft.AspNetCore.Http;

namespace GameStuff.Models
{
    public class GamesGridBuilder : GridBuilder
    {
        public GamesGridBuilder(ISession sess) : base(sess) { }

        public GamesGridBuilder(ISession sess, GamesGridDTO values, 
            string defaultSortField) : base(sess, values, defaultSortField)
        {
            bool isInitial = values.Genre.IndexOf(FilterPrefix.Genre) == -1;
            routes.DeveloperFilter = (isInitial) ? FilterPrefix.Developer + values.Developer : values.Developer;
            routes.GenreFilter = (isInitial) ? FilterPrefix.Genre + values.Genre : values.Genre;
            routes.PriceFilter = (isInitial) ? FilterPrefix.Price + values.Price : values.Price;
        }

        public void LoadFilterSegments(string[] filter, Developer developer)
        {
            if (developer == null) { 
                routes.DeveloperFilter = FilterPrefix.Developer + filter[0];
            } else {
                routes.DeveloperFilter = FilterPrefix.Developer + filter[0]
                    + "-" + developer.FullName.Slug();
            }
            routes.GenreFilter = FilterPrefix.Genre + filter[1];
            routes.PriceFilter = FilterPrefix.Price + filter[2];
        }
        public void ClearFilterSegments() => routes.ClearFilters();

        string def = GamesGridDTO.DefaultFilter;   
        public bool IsFilterByDeveloper => routes.DeveloperFilter != def;
        public bool IsFilterByGenre => routes.GenreFilter != def;
        public bool IsFilterByPrice => routes.PriceFilter != def;

        public bool IsSortByGenre =>
            routes.SortField.EqualsNoCase(nameof(Genre));
        public bool IsSortByPrice =>
            routes.SortField.EqualsNoCase(nameof(Game.Price));
    }
}
