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
            if(!ModelState.IsValid)
            {
                return View(materias);
            }
            var existe = await repositorioMaterias.Existe(materias.Nombre);

            if(existe)
            {
                ModelState.AddModelError(nameof(materias.Nombre),
                    $"El nombre {materias.Nombre} ya existe");
                return View(materias);
            }

            await repositorioMaterias.Crear(materias);
            return View();
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
            var materiaExiste = await repositorioMaterias.ObtenerPorId(materias.Id);

            if (materiaExiste is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioMaterias.Actualizar(materias);
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
            var materia = await repositorioMaterias.ObtenerPorId(id);

            if (materia is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioMaterias.Borrar(id);
            return RedirectToAction("Index");
        }
    }
}
