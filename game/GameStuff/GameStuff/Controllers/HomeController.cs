using System;
using Microsoft.AspNetCore.Mvc;
using GameStuff.Models;

namespace GameStuff.Controllers
{
    public class HomeController : Controller
    {
        private Repository<Game> data { get; set; }
        public HomeController(GameStuffContext ctx) => data = new Repository<Game>(ctx);

        public ViewResult Index()
        {
            var random = data.Get(new QueryOptions<Game> {
                OrderBy = b => Guid.NewGuid()
            });

            return View(random);
        }
    }
}