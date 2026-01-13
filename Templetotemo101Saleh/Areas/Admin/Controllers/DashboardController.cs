using Microsoft.AspNetCore.Mvc;

namespace Templetotemo101Saleh.Areas.Admin.Controllers;
[Area("Admin")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
