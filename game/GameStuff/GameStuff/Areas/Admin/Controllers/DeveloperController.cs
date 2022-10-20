using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GameStuff.Models;

namespace GameStuff.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class DeveloperController : Controller
    {
        private Repository<Developer> data { get; set; }
        public DeveloperController(GameStuffContext ctx) => data = new Repository<Developer>(ctx);

        public ViewResult Index()
        {
            var developers = data.List(new QueryOptions<Developer> {
                OrderBy = a => a.FirstName
            });
            return View(developers);
        }

        public RedirectToActionResult Select(int id, string operation)
        {
            switch (operation.ToLower())
            {
                case "view games":
                    return RedirectToAction("ViewGames", new { id });
                case "edit":
                    return RedirectToAction("Edit", new { id });
                case "delete":
                    return RedirectToAction("Delete", new { id });
                default:
                    return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ViewResult Add() => View("Developer", new Developer());

        [HttpPost]
        public IActionResult Add(Developer developer, string operation)
        {
            var validate = new Validate(TempData);
            if (!validate.IsDeveloperChecked) {
                validate.CheckDeveloper(developer.FirstName, developer.LastName, operation, data);
                if (!validate.IsValid) {
                    ModelState.AddModelError(nameof(developer.LastName), validate.ErrorMessage);
                }    
            }
            
            if (ModelState.IsValid) {
                data.Insert(developer);
                data.Save();
                validate.ClearDeveloper();
                TempData["message"] = $"{developer.FullName} added to Developers.";
                return RedirectToAction("Index");  
            }
            else {
                return View("Developer", developer);
            }
        }

        [HttpGet]
        public ViewResult Edit(int id) => View("Developer", data.Get(id));

        [HttpPost]
        public IActionResult Edit(Developer developer)
        {
            if (ModelState.IsValid) {
                data.Update(developer);
                data.Save();
                TempData["message"] = $"{developer.FullName} updated.";
                return RedirectToAction("Index");  
            }
            else {
                return View("Developer", developer);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var developer = data.Get(new QueryOptions<Developer> {
                Include = "GameDevelopers",
                Where = a => a.DeveloperId == id
            });

            if (developer.GameDevelopers.Count > 0) {
                TempData["message"] = $"Can't delete developer {developer.FullName} ";
                return GoToDeveloperSearch(developer);
            }
            else {
                return View("Developer", developer);
            }
        }

        [HttpPost]
        public RedirectToActionResult Delete(Developer developer)
        {
            data.Delete(developer);
            data.Save();
            TempData["message"] = $"{developer.FullName} removed from Developers.";
            return RedirectToAction("Index");  
        }

        public RedirectToActionResult ViewGames(int id)
        {
            var developer = data.Get(id);
            return GoToDeveloperSearch(developer);
        }

        private RedirectToActionResult GoToDeveloperSearch(Developer developer)
        {
            var search = new SearchData(TempData) {
                SearchTerm = developer.FullName,
                Type = "developer"
            };
            return RedirectToAction("Search", "Game");
        }
    }
}