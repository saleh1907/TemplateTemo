using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Templetotemo101Saleh.Context;
using Templetotemo101Saleh.Models;
using Templetotemo101Saleh.ViewModels.ProductViewModels;

namespace Templetotemo101Saleh.Areas.Admin.Controllers;
[Area("Admin")]

public class ProductController(AppDbContext _context,IWebHostEnvironment _environment) : Controller
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
    public async Task<IActionResult> Create()
    {
        await _sendCategoriesWithBag();

        return View();


    }


    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateVM vm)
    {
        await _sendCategoriesWithBag();
        if (!ModelState.IsValid)
        {
            return View(vm);
        }
        var isExistCategory = await _context.Categories.AnyAsync(x => x.Id == vm.CategoryId);

        if (!isExistCategory)
        {
            ModelState.AddModelError("CategoryId","This category is not found");

            return View(vm);
        }
        if (vm.Image.Length > 2 * 1024 * 1024)
        {
            ModelState.AddModelError("Image","Image's maximun size must be 2 mb");
            return View(vm);
}
        if (vm.Image.ContentType.Contains("image"))
        {
            ModelState.AddModelError("Image","You can upload file only image format");

            return View(vm);
}
        string uniqueFileName = Guid.NewGuid().ToString() + vm.Image.FileName;
        string folderPath = Path.Combine(_environment.WebRootPath, "assets", "images");

        string path = Path.Combine(folderPath, uniqueFileName);

        using FileStream stream = new(path, FileMode.Create);

        await vm.Image.CopyToAsync(stream);
        Product product = new()
        {
            Name = vm.Name,
            Description = vm.Description,
            Price = vm.Price,
            Rating = vm.Rating,
            CategoryId = vm.CategoryId,
            ImagePath= uniqueFileName
        };

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
    private async Task _sendCategoriesWithBag()
    {
        var categories = await _context.Categories.Select(c => new SelectListItem()
        {
            Value = c.Id.ToString(),
            Text = c.Name,

        }).ToListAsync();

        ViewBag.Categories = categories;
    }
}