using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Templetotemo101Saleh.Context;
using Templetotemo101Saleh.ViewModels.ProductViewModels;

namespace Templetotemo101Saleh.Areas.Admin.Controllers;
[Area("Admin")]

public class ProductController(AppDbContext _context) : Controller
{
    public async Task<IActionResult> Index()
    {
        var products = await _context.Products.Select(x=>new ProductGetVM()
        {
            Id=x.Id,
            Name=x.Name,
            Description=x.Description,
            Price=x.Price,
            CategoryName=x.Category.Name,
            ImagePath=x.ImagePath
        }).ToListAsync();
        return View(products);
    }
    public  async Task<IActionResult> Create()
    {
        var categories = await _context.Categories.Select(c=>new SelectListItem()
        {
            Value=c.Id.ToString(),
            Text=c.Name,

        }).ToListAsync();

        ViewBag.Categories = categories;    
        return View();
    }
}
