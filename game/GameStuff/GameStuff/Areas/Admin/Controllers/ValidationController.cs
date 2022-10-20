using Microsoft.AspNetCore.Mvc;
using GameStuff.Models;

namespace GameStuff.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ValidationController : Controller
    {
        private Repository<Developer> DeveloperData { get; set; }
        private Repository<Genre> genreData { get; set; }

        public ValidationController(GameStuffContext ctx)
        { 
            DeveloperData = new Repository<Developer>(ctx);
            genreData = new Repository<Genre>(ctx);
        }

        public JsonResult CheckGenre(string genreId)
        {
            var validate = new Validate(TempData);
            validate.CheckGenre(genreId, genreData);
            if (validate.IsValid) {
                validate.MarkGenreChecked();
                return Json(true);
            }
            else {
                return Json(validate.ErrorMessage);
            }
        }

        public JsonResult CheckAuthor(string firstName, string lastName, string operation)
        {
            var validate = new Validate(TempData);
            validate.CheckDeveloper(firstName, lastName, operation, DeveloperData);
            if (validate.IsValid) {
                validate.MarkDeveloperChecked();
                return Json(true);
            }
            else {
                return Json(validate.ErrorMessage);
            }
        }
    }
}