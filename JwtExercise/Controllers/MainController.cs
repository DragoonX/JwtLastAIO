using Microsoft.AspNetCore.Mvc;

namespace JwtExercise.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}