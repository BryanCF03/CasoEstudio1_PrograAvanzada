using System;
using System.Web.Mvc;
using Habitaciones.LogicaDeNegocio;
using Habitaciones.LogicaDeNegocio.DTO;

namespace CasoDeEstudio_1.Controllers
{
    public class HabitacionesController : Controller
    {
        private readonly HabitacionLN habitacionLN = new HabitacionLN();

        // GET: Habitaciones
        public ActionResult Index()
        {
            var habitaciones = habitacionLN.Listar();

            return View(habitaciones);
        }

        // GET: Habitaciones/Create
        public ActionResult Create()
        {
            return View(new HabitacionDTO());
        }

        // POST: Habitaciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HabitacionDTO habitacion)
        {
            if (!ModelState.IsValid)
            {
                return View(habitacion);
            }

            try
            {
                habitacionLN.Registrar(habitacion);

                TempData["Mensaje"] =
                    "La habitación se registró correctamente.";

                return RedirectToAction("Index");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(habitacion);
            }
            catch (Exception)
            {
                ModelState.AddModelError(
                    "",
                    "Ocurrió un error al registrar la habitación. Verifique los datos e inténtelo nuevamente."
                );

                return View(habitacion);
            }
        }
    }
}