using System.Collections.Generic;

namespace GameStuff.Models
{
    public class CartViewModel 
    {
        public IEnumerable<CartItem> List { get; set; }
        public double Subtotal { get; set; }
        public RouteDictionary GameGridRoute { get; set; }
    }
}
