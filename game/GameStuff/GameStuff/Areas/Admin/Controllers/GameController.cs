using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GameStuff.Models;

namespace GameStuff.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class GameController : Controller
    {
        private GameStuffUnitOfWork data { get; set; }
        public GameController(GameStuffContext ctx) => data = new GameStuffUnitOfWork(ctx);

        public ViewResult Index() {
            var search = new SearchData(TempData);
            search.Clear();

            return View();
        }

        [HttpPost]
        public RedirectToActionResult Search(SearchViewModel vm)
        {
            if (ModelState.IsValid) {
                var search = new SearchData(TempData) {
                    SearchTerm = vm.SearchTerm,
                    Type = vm.Type
                };
                return RedirectToAction("Search");
            }  
            else {
                return RedirectToAction("Index");
            }   
        }

        [HttpGet]
        public ViewResult Search() 
        {
            var search = new SearchData(TempData);

            if (search.HasSearchTerm) {
                var vm = new SearchViewModel {
                    SearchTerm = search.SearchTerm
                };

                var options = new QueryOptions<Game> {
                    Include = "Genre, GameDevelopers.Developer"
                };
                if (search.IsGame) { 
                    options.Where = b => b.Title.Contains(vm.SearchTerm);
                    vm.Header = $"Search results for game title '{vm.SearchTerm}'";
                }
                if (search.IsDeveloper) {
                    int index = vm.SearchTerm.LastIndexOf(' ');
                    if (index == -1)
                    {
                        options.Where = b => b.GameDevelopers.Any(
                            ba => ba.Developer.DevName.Contains(vm.SearchTerm));
                    }
                    else {
                        string first = vm.SearchTerm.Substring(0, index);
                        string last = vm.SearchTerm.Substring(index + 1);
                        options.Where = b => b.GameDevelopers.Any(
                            ba => ba.Developer.DevName.Contains(first));

                    }
                    vm.Header = $"Search results for developer '{vm.SearchTerm}'";
                }
                if (search.IsGenre) {                  
                    options.Where = b => b.GenreId.Contains(vm.SearchTerm);
                    vm.Header = $"Search results for genre ID '{vm.SearchTerm}'";
                }
                vm.Games = data.Games.List(options);
                return View("SearchResults", vm);
            }
            else {
                return View("Index");
            }     
        }

        [HttpGet]
        public ViewResult Add(int id) => GetGame(id, "Add");

        [HttpPost]
        public IActionResult Add(GameViewModel vm)
        {
            if (ModelState.IsValid) {
                data.LoadNewGameDevelopers(vm.Game, vm.SelectedDevelopers);
                data.Games.Insert(vm.Game);
                data.Save();

                TempData["message"] = $"{vm.Game.Title} added to Games.";
                return RedirectToAction("Index");  
            }
            else {
                Load(vm, "Add");
                return View("Game", vm);
            }
        }

        [HttpGet]
        public ViewResult Edit(int id) => GetGame(id, "Edit");
        
        [HttpPost]
        public IActionResult Edit(GameViewModel vm)
        {
            if (ModelState.IsValid) {
                data.DeleteCurrentGameDevelopers(vm.Game);
                data.LoadNewGameDevelopers(vm.Game, vm.SelectedDevelopers);
                data.Games.Update(vm.Game);
                data.Save(); 
                
                TempData["message"] = $"{vm.Game.Title} updated.";
                return RedirectToAction("Search");  
            }
            else {
                Load(vm, "Edit");
                return View("Game", vm);
            }
        }

        [HttpGet]
        public ViewResult Delete(int id) => GetGame(id, "Delete");

        [HttpPost]
        public IActionResult Delete(GameViewModel vm)
        {
            data.Games.Delete(vm.Game); 
            data.Save();
            TempData["message"] = $"{vm.Game.Title} removed from Games.";
            return RedirectToAction("Search");  
        }

        private ViewResult GetGame(int id, string operation)
        {
            var game = new GameViewModel();
            Load(game, operation, id);
            return View("Game", game);
        }
        private void Load(GameViewModel vm, string op, int? id = null)
        {
            if (Operation.IsAdd(op)) { 
                vm.Game = new Game();
            }
            else {
                vm.Game = data.Games.Get(new QueryOptions<Game>
                {
                    Include = "GameDevelopers.Developer, Genre",
                    Where = b => b.GameId == (id ?? vm.Game.GameId)
                });
            }

            vm.SelectedDevelopers = vm.Game.GameDevelopers?.Select(
                ba => ba.Developer.DeveloperId).ToArray();
            vm.Developers = data.Developers.List(new QueryOptions<Developer> {
                OrderBy = a => a.DevName });
            vm.Genres = data.Genres.List(new QueryOptions<Genre> {
                    OrderBy = g => g.Name });
        }

    }
}