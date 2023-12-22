using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIGA.Models;
using SIGA.Services;

namespace SIGA.Controllers
{
    [Authorize]
    public class MateriasController: Controller
    {
        private readonly IRepositorioMaterias repositorioMaterias;

        public MateriasController(IRepositorioMaterias repositorioMaterias) 
        {
            this.repositorioMaterias = repositorioMaterias;
        }

        public async Task<IActionResult> Index()
        {
            string mensaje = TempData["sms"] as string;
            ViewBag.sms = mensaje;

            string mensajeEditar = TempData["smsEditar"] as string;
            ViewBag.smsEditar = mensajeEditar;

            var materias = await repositorioMaterias.Obtener();
            return View(materias);
        }
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Crear(Materias materias)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(materias);
                }

                await repositorioMaterias.Crear(materias);
                TempData["sms"] = "Se ha registrado correctamente la materia";
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
            var materia = await repositorioMaterias.ObtenerPorId(id);

            if (materia is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(materia);
        }

        [HttpPost]
        public async Task<ActionResult> Editar(Materias materias)
        {
            if (!ModelState.IsValid)
            {
                return View(materias);
            }
            var materiaExiste = await repositorioMaterias.ObtenerPorId(materias.Id);

            if (materiaExiste is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioMaterias.Actualizar(materias);
            TempData["smsEditar"] = "Se ha actualizado correctamente la materia";
            ViewBag.smsEditar = TempData;
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Borrar(int id)
        {
            var materia = await repositorioMaterias.ObtenerPorId(id);

            if (materia is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(materia);
        }

        [HttpPost]

        public async Task<IActionResult> BorrarMateria(int id)
        {
            try
            {
                var materia = await repositorioMaterias.ObtenerPorId(id);

                await repositorioMaterias.Borrar(id);
                if (materia is null)
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
