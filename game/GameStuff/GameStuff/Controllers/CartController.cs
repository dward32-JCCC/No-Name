using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GameStuff.Models;

namespace GameStuff.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private Repository<Game> data { get; set; }
        public CartController(GameStuffContext ctx) => data = new Repository<Game>(ctx);


        private Cart GetCart()
        {
            var cart = new Cart(HttpContext);
            cart.Load(data);
            return cart;
        }

        public ViewResult Index() 
        {
            var cart = GetCart();
            var builder = new GamesGridBuilder(HttpContext.Session);

            var vm = new CartViewModel {
                List = cart.List,
                Subtotal = cart.Subtotal,
                GameGridRoute = builder.CurrentRoute
            };
            return View(vm);
        }

        [HttpPost]
        public RedirectToActionResult Add(int id)
        {
            var game = data.Get(new QueryOptions<Game> {
                Include = "GameDevelopers.Developer, Genre",
                Where = b => b.GameId == id
            });
            if (game == null){
                TempData["message"] = "Unable to add game to cart.";   
            }
            else {
                var dto = new GameDTO();
                dto.Load(game);
                CartItem item = new CartItem {
                    Game = dto,
                    Quantity = 1  // default value
                };

                Cart cart = GetCart();
                cart.Add(item);
                cart.Save();

                TempData["message"] = $"{game.Title} added to cart";
            }

            var builder = new GamesGridBuilder(HttpContext.Session);
            return RedirectToAction("List", "Game", builder.CurrentRoute);
        }

        [HttpPost]
        public RedirectToActionResult Remove(int id)
        {
            Cart cart = GetCart();
            CartItem item = cart.GetById(id);
            cart.Remove(item);
            cart.Save();

            TempData["message"] = $"{item.Game.Title} removed from cart.";
            return RedirectToAction("Index");
        }
                
        [HttpPost]
        public RedirectToActionResult Clear()
        {
            Cart cart = GetCart();
            cart.Clear();
            cart.Save();

            TempData["message"] = "Cart cleared.";
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {
            Cart cart = GetCart();
            CartItem item = cart.GetById(id);
            if (item == null)
            {
                TempData["message"] = "Unable to locate cart item";
                return RedirectToAction("List");
            }
            else
            {
                return View(item);
            }
        }

        [HttpPost]
        public RedirectToActionResult Edit(CartItem item)
        {
            Cart cart = GetCart();
            cart.Edit(item);
            cart.Save();

            TempData["message"] = $"{item.Game.Title} updated";
            return RedirectToAction("Index");
        }

        public ViewResult Checkout() => View();
    }
}