using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Templetotemo101Saleh.Models;
using Templetotemo101Saleh.ViewModels.UserViewModels;

namespace Templetotemo101Saleh.Controllers;

public class AccountController(UserManager<AppUser>_userManager,SignInManager<AppUser>_signInManager) : Controller
{
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM vm)
    {
        if(!ModelState.IsValid) 
            return View(vm);

        AppUser user = new()
        {
            Email = vm.Email,
            UserName = vm.UserName,
            FullName = vm.FullName
        };
   
         var result=await _userManager.CreateAsync(user,vm.Password);
        if(result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);

            }
            return View(vm);
        }

        await _signInManager.SignInAsync(user, false);
        return RedirectToAction("Index","Home");

    }
    public IActionResult Login()
    {
        return View();
    }
    public async Task<IActionResult> Login(LoginVM vm)
    {
        if(!ModelState.IsValid) 
            return View(vm);

        var user=await _userManager.FindByEmailAsync(vm.Email);
        if(user is null)
        {
            ModelState.AddModelError("","email or password is wrong");
            return View(vm);
        }
        var result = await _signInManager.PasswordSignInAsync(user, vm.Password, false, true);

        if(!result.Succeeded)
        {
            ModelState.AddModelError("", "email or password is wrong");
            return View(vm);
        }
        return RedirectToAction("Index","Home");
    }
}
