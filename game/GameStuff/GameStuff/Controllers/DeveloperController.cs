using Microsoft.AspNetCore.Mvc;
using GameStuff.Models;

namespace GameStuff.Controllers
{
    public class DeveloperController : Controller
    {
        private Repository<Developer> data { get; set; }
        public DeveloperController(GameStuffContext ctx) => data = new Repository<Developer>(ctx);

        public IActionResult Index() => RedirectToAction("List");

        public ViewResult List(GridDTO vals)
        {
            string defaultSort = nameof(Developer.DevName);
            var builder = new GridBuilder(HttpContext.Session, vals, defaultSort);
            builder.SaveRouteSegments();

            var options = new QueryOptions<Developer> {
                Include = "GameDevelopers.Game",
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize,
                OrderByDirection = builder.CurrentRoute.SortDirection
            };
            if (builder.CurrentRoute.SortField.EqualsNoCase(defaultSort))
                options.OrderBy = a => a.DevName;
            else
                options.OrderBy = a => a.DevName;

            var vm = new DeveloperListViewModel {
                Developers = data.List(options),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Count)
            };

            return View(vm);
        }

        public IActionResult Details(int id)
        {
            var developer = data.Get(new QueryOptions<Developer> {
                Include = "GameDevelopers.Game",
                Where = a => a.DeveloperId == id
            });
            return View(developer);
        }
    }
}