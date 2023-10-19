using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using SIGA.Models;
using SIGA.Services;

namespace SIGA.Controllers
{
    public class UsersController: Controller
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
        public IActionResult Login(LoginViewModel model)
        {
            User us = repositoryUsers.FindUser(model.Email, model.Password);

            if(us.email != null)
            {
                return PartialView("_MenuPartial");
                
            }
            else
            {
                ViewData["Mensaje"] = "Credenciales incorrectas";
                return View();
            }
        }
    }
}
