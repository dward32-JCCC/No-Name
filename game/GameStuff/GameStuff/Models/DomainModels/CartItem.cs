using Newtonsoft.Json;

namespace GameStuff.Models
{
    public class CartItem
    {
        public GameDTO Game { get; set; }
        public int Quantity { get; set; }

        [JsonIgnore]
        public double Subtotal => Game.Price * Quantity;
    }
}
