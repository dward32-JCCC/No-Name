using System.Linq;
using System.Collections.Generic;

namespace GameStuff.Models
{
    public static class CartItemListExtensions
    {
        public static List<CartItemDTO> ToDTO(this List<CartItem> list) =>
            list.Select(ci => new CartItemDTO {
                GameId = ci.Game.GameId,
                Quantity = ci.Quantity
            }).ToList();
    }
}
