using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GameStuff.Models;

namespace GameStuff.Controllers
{
    public class GameController : Controller
    {
        private GameStuffUnitOfWork data { get; set; }
        public GameController(GameStuffContext ctx) => data = new GameStuffUnitOfWork(ctx);

        public RedirectToActionResult Index() => RedirectToAction("List");

        public ViewResult SearchResults()
        {
            var search = new SearchData(TempData);
            search.Clear();

            return View();
        }

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


        [HttpPost]
        public RedirectToActionResult Search(GameListViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var search = new SearchData(TempData)
                {
                    SearchTerm = vm.SearchTerm,
                    Type = vm.Type
                };
                return RedirectToAction("Search");
            }
            else
            {
                return RedirectToAction("SearchResults");
            }
        }

        [HttpGet]
        public ViewResult Search()
        {
            var search = new SearchData(TempData);

            if (search.HasSearchTerm)
            {
                var vm = new GameListViewModel
                {
                    SearchTerm = search.SearchTerm
                };

                var options = new QueryOptions<Game>
                {
                    Include = "Genre, GameDevelopers.Developer"
                };
                if (search.IsGame)
                {
                    options.Where = b => b.Title.Contains(vm.SearchTerm);
                    vm.Header = $"Search results for game title '{vm.SearchTerm}'";
                }
                if (search.IsDeveloper)
                {
                    int index = vm.SearchTerm.LastIndexOf(' ');
                    if (index == -1)
                    {
                        options.Where = b => b.GameDevelopers.Any(
                            ba => ba.Developer.DevName.Contains(vm.SearchTerm));
                    }
                    else
                    {
                        string first = vm.SearchTerm.Substring(0, index);
                        string last = vm.SearchTerm.Substring(index + 1);
                        options.Where = b => b.GameDevelopers.Any(
                            ba => ba.Developer.DevName.Contains(first));

                    }
                    vm.Header = $"Search results for developer '{vm.SearchTerm}'";
                }
                if (search.IsGenre)
                {
                    options.Where = b => b.GenreId.Contains(vm.SearchTerm);
                    vm.Header = $"Search results for genre ID '{vm.SearchTerm}'";
                }
                vm.Games = data.Games.List(options);
                return View("SearchResults", vm);
            }
            else
            {
                return View("Index");
            }
        }
    }   
}