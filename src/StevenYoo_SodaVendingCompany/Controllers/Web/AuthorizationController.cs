using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StevenYoo_SodaVendingCompany.Models;
using StevenYoo_SodaVendingCompany.ViewModels;

namespace StevenYoo_SodaVendingCompany.Controllers.Web
{
    public class AuthorizationController : Controller
    {
        SignInManager<User> _signInManager;
        public AuthorizationController(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "App");
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel vm, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                var signInResult = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, true, false);

                if (signInResult.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("Index", "App");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }

                }
            }
            else
            {
                ModelState.AddModelError("", "Username or password incorrect");
            }

            return View();
        }

        public async Task<ActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }

            return RedirectToAction("Index", "App");
        }
    }
}

