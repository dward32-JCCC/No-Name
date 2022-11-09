using System.Collections.Generic;

namespace GameStuff.Models
{
    public class GameListViewModel 
    {
        public IEnumerable<Game> Games { get; set; }
        public RouteDictionary CurrentRoute { get; set; }
        public int TotalPages { get; set; }

        // data for filter drop-downs - one hardcoded
        public IEnumerable<Developer> Developers { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public Dictionary<string, string> Prices =>
            new Dictionary<string, string> {
                { "0-20", "$0-$20" },
                { "20-70", "$20-$70" },
                { "over70", "Over $70" }
            };
    }
}
