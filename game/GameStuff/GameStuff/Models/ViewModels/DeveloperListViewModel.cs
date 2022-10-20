using System.Collections.Generic;

namespace GameStuff.Models
{
    public class DeveloperListViewModel
    {
        public IEnumerable<Developer> Developers { get; set; }
        public RouteDictionary CurrentRoute { get; set; }
        public int TotalPages { get; set; }
    }
}
