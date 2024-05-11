using Microsoft.AspNetCore.Mvc;

namespace crudlab.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
