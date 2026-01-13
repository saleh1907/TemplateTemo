using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;


namespace Templetotemo101Saleh.Controllers
{
    public class HomeController : Controller
    {
       public IActionResult Index()
        {
            return View();
        }
    }
}
