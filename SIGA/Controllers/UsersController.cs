using Microsoft.AspNetCore.Mvc;
using SIGA.Models;
using SIGA.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace SIGA.Controllers
{
    
    public class UsersController : Controller
    {
        private readonly IRepositoryUsers repositoryUsers;

        public UsersController(IRepositoryUsers repositoryUsers)
        {
            this.repositoryUsers = repositoryUsers;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            User us = repositoryUsers.FindUser(model.Email, model.Password);

            if (us.email != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, us.email),
                };

                foreach (string item in Enum.GetNames(typeof(Role)))
                {
                    claims.Add(new Claim(ClaimTypes.Role, item));
                }

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");

            }


            ModelState.AddModelError("", "Credenciales de acceso incorrectas");
            return View();

        }

        public async Task<IActionResult> Out()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Users");
        }

    }
}
