using Microsoft.AspNetCore.Mvc;
using MvcApiPersonajesExamen.Models;
using MvcApiPersonajesExamen.Services;

namespace MvcApiPersonajesExamen.Controllers
{
    public class PersonajesController : Controller
    {
        private ServicePersonajes service;

        public PersonajesController(ServicePersonajes service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Personajes()
        {
            List<Personaje> personajes =
                await this.service.GetPersonajesAsync();
            return View(personajes);
        }

        public async Task<IActionResult> Detalles(int idpersonaje)
        {
            Personaje personaje =
                await this.service.FindPersonajeAsync(idpersonaje);
            return View(personaje);
        }

        public async Task<IActionResult> PersonajesSerie(string serie)
        {
            List<Personaje> personajesSerie =
                await this.service.FindPersonajesSeriesBySerie(serie);
            return View(personajesSerie);
        }

        public async Task<IActionResult> Series()
        {
            List<string> series =
                await this.service.GetAllSeries();
            return View(series);
        }

        public IActionResult InsertPersonaje()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> InsertPersonaje(Personaje personaje)
        {
            await this.service.InsertPersonajeAsync(personaje.Nombre, personaje.Imagen, personaje.Serie);
            return RedirectToAction("Personajes");
        }

        public async Task<IActionResult> UpdatePersonaje(int idpersonaje)
        {


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePersonaje(Personaje personaje)
        {
            return RedirectToAction("Personajes");
        }
    }
}
