using Microsoft.AspNetCore.Mvc;
using GameStuff.Models;

namespace GameStuff.Controllers
{
    public class GameController : Controller
    {
        private GameStuffUnitOfWork data { get; set; }
        public GameController(GameStuffContext ctx) => data = new GameStuffUnitOfWork(ctx);

        public RedirectToActionResult Index() => RedirectToAction("List");

        public ViewResult List(GamesGridDTO values)
        {
            var builder = new GamesGridBuilder(HttpContext.Session, values, 
                defaultSortField: nameof(Game.Title));

            var options = new GameQueryOptions {
                Include = "GameDevelopers.Developer, Genre",
                OrderByDirection = builder.CurrentRoute.SortDirection,
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize
            };
            options.SortFilter(builder);

            var vm = new GameListViewModel {
                Games = data.Games.List(options),
                Developers = data.Developers.List(new QueryOptions<Developer> {
                    OrderBy = a => a.DevName }),
                Genres = data.Genres.List(new QueryOptions<Genre> {
                    OrderBy = g => g.Name }),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Games.Count)
            };

            return View(vm);
        }

        public ViewResult Details(int id)
        {
            var game = data.Games.Get(new QueryOptions<Game> {
                Include = "GameDevelopers.Developer, Genre",
                Where = b => b.GameId == id
            });
            return View(game);
        }

        [HttpPost]
        public RedirectToActionResult Filter(string[] filter, bool clear = false)
        {
            var builder = new GamesGridBuilder(HttpContext.Session);

            if (clear) {
                builder.ClearFilterSegments();
            }
            else {
                var developer = data.Developers.Get(filter[0].ToInt());
                builder.LoadFilterSegments(filter, developer);
            }

            builder.SaveRouteSegments();
            return RedirectToAction("List", builder.CurrentRoute);
        }
    }   
}