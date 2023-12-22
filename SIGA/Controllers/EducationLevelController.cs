using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIGA.Models;
using SIGA.Services;

namespace SIGA.Controllers
{
    [Authorize]
    public class EducationLevelController: Controller
    {
        private readonly IRepositoryEducationLevel repositoryEducationLevel;

        public EducationLevelController(IRepositoryEducationLevel repositoryEducationLevel) 
        {
            this.repositoryEducationLevel = repositoryEducationLevel;
        }

        public async Task<IActionResult> Index()
        {
            string mensaje = TempData["sms"] as string;
            ViewBag.sms = mensaje;

            string mensajeEditar = TempData["smsEditar"] as string;
            ViewBag.smsEditar = mensajeEditar;
            /*try
            {
                ViewBag.sms = TempData["sms"].ToString();
            }
            catch { }*/
            var educationLevel = await repositoryEducationLevel.Obtener();
            return View(educationLevel);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task <IActionResult> Create(EducationLevel educationLevel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(educationLevel);
                }
                /*
                var existe = await repositoryEducationLevel.Existe(educationLevel.Nombre);
                
                if (existe)
                {
                    ModelState.AddModelError(nameof(educationLevel.Nombre),
                        $"El nombre {educationLevel.Nombre} ya existe.");
                    return View(educationLevel);
                }
                */
                await repositoryEducationLevel.Crear(educationLevel);
                TempData["sms"] = "Se ha registrado correctamente el nivel educativo";
                ViewBag.sms = TempData["sms"];
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["sms"] = "Error en el registro";
                ViewBag.sms = TempData["sms"];
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Editar(int id)
         {
            var nivelEducativo = await repositoryEducationLevel.ObtenerPorId(id);

            if(nivelEducativo is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(nivelEducativo);
        }

        [HttpPost]
        public async Task<ActionResult>Editar(EducationLevel educationLevel)
           
        {
            if (!ModelState.IsValid)
            {
                return View(educationLevel);
            }

            var nivelEducativoExiste = await repositoryEducationLevel.ObtenerPorId(educationLevel.Id);


            if (nivelEducativoExiste is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositoryEducationLevel.Actualizar(educationLevel);
            TempData["smsEditar"] = "Se ha actualizado correctamente el nivel educativo";
            ViewBag.smsEditar = TempData;
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Borrar(int id)
        {
            var educationLevel = await repositoryEducationLevel.ObtenerPorId(id);

            if (educationLevel is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(educationLevel);
        }

        [HttpPost]

        public async Task<JsonResult> BorrarNivelEducativo(int id)
        {
            
            try
            {
                var educationLevel = await repositoryEducationLevel.ObtenerPorId(id);

                await repositoryEducationLevel.Borrar(id);
                if (educationLevel is null)
                {
                    return Json(new { success = true, status = 404, message = "Registro no encontrado." });
                    
                }
                else
                {
                    return Json(new { success = true, status = 200, message = "Datos eliminados exitosamente." });
                }
            }

            catch (Exception ex)
            {

                
                return Json(new { success = false, status = 500, message = "Error al eliminar datos: " + ex.Message });
            }


        }
    }
}
