using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIGA.Models;
using SIGA.Services;

namespace SIGA.Controllers
{
    [Authorize]
    public class GruposController : Controller

    {
        private readonly IRepositorioGrupos repositorioGrupos;

        public GruposController(IRepositorioGrupos repositorioGrupos)
        {
            this.repositorioGrupos = repositorioGrupos;
        }
        public async Task<IActionResult> Index()
        {
            var materias = await repositorioGrupos.Obtener();
            return View(materias);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Grupos grupos)
        {
            if (!ModelState.IsValid)
            {
                return View(grupos);
            }
            var existe = await repositorioGrupos.Existe(grupos.Nombre);

            if (existe)
            {
                ModelState.AddModelError(nameof(grupos.Nombre),
                    $"El nombre {grupos.Nombre} ya existe");
                return View(grupos);
            }

            await repositorioGrupos.Crear(grupos);
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Editar(int id)
        {
            var grupo = await repositorioGrupos.ObtenerPorId(id);

            if (grupo is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(grupo);
        }

        [HttpPost]
        public async Task<ActionResult> Editar(Grupos grupos)
        {
            var grupoExiste = await repositorioGrupos.ObtenerPorId(grupos.Id);

            if (grupoExiste is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioGrupos.Actualizar(grupos);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Borrar(int id)
        {
            var grupo = await repositorioGrupos.ObtenerPorId(id);

            if (grupo is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(grupo);
        }

        [HttpPost]

        public async Task<IActionResult> BorrarGrupo(int id)
        {
            var grupo = await repositorioGrupos.ObtenerPorId(id);

            if (grupo is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioGrupos.Borrar(id);
            return RedirectToAction("Index");
        }
    }

}
