using Microsoft.AspNetCore.Mvc;

namespace crudlab.Controllers;

public class StudentController : Controller
{
    public IActionResult Index()
    {
        return View();
    }


}
