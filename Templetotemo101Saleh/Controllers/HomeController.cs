using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;
using Templetotemo101Saleh.Context;
using Templetotemo101Saleh.ViewModels.ProductViewModels;


namespace Templetotemo101Saleh.Controllers
{
    public class HomeController(AppDbContext _context) : Controller
    {
       public async Task<IActionResult> Index()
        {
            var products = await _context.Products.Select(x => new ProductGetVM()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                CategoryName = x.Category.Name,
                ImagePath = x.ImagePath
            }).ToListAsync();
            return View(products);
        }
    }
}
