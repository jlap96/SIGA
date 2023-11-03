using Microsoft.AspNetCore.Mvc;
using SIGA.Models;

namespace SIGA.Controllers
{
    public class EducationLevelController: Controller
    {
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(EducationLevel educationLevel)
        {
            if(!ModelState.IsValid)
            {
                return View(educationLevel);
            }
            return View();
        }
    }
}
