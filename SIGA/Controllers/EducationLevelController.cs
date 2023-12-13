﻿using Microsoft.AspNetCore.Authorization;
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

                var existe = await repositoryEducationLevel.Existe(educationLevel.Nombre);

                if (existe)
                {
                    ModelState.AddModelError(nameof(educationLevel.Nombre),
                        $"El nombre {educationLevel.Nombre} ya existe.");
                    return View(educationLevel);
                }
                
                await repositoryEducationLevel.Crear(educationLevel);
                TempData["sms"] = "Se ha registrado correctamente el nivel educativo";
                ViewBag.sms = "La acción se realizó correctamente";
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
        public async Task<ActionResult> EditarNivelEducativo(EducationLevel educationLevel)
        {
            var nivelEducativoExiste = await repositoryEducationLevel.ObtenerPorId(educationLevel.Id);

            if(nivelEducativoExiste is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositoryEducationLevel.Actualizar(educationLevel);
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

        public async Task<IActionResult> BorrarNivelEducativo(int id)
        {
            var educationLevel = await repositoryEducationLevel.ObtenerPorId(id);

            if (educationLevel is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositoryEducationLevel.Borrar(id);
            return RedirectToAction("Index");
        }
    }
}
