using Reservaciones.Abstracciones.LogicaDeNegocio.Reservaciones;
using Reservaciones.Abstracciones.ModelosParaUI;
using Reservaciones.LogicaDeNegocio.Reservaciones;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Web.Mvc;

namespace Semana1_Framework.Controllers
{
    public class ReservacionesController : Controller
    {
        private readonly IReservacionesLN _reservacionesLN;

        public ReservacionesController()
        {
            _reservacionesLN = new ReservacionesLN();
        }

        // Página inicial del módulo
        public ActionResult Index()
        {
            return RedirectToAction("ListarHabitacionesDisponibles");
        }

        // Lista las habitaciones disponibles
        [HttpGet]
        public ActionResult ListarHabitacionesDisponibles()
        {
            List<HabitacionReservaDto> habitaciones =
                _reservacionesLN.ObtenerHabitacionesDisponibles();

            return View(habitaciones);
        }

        // Muestra el formulario para reservar una habitación
        [HttpGet]
        public ActionResult Reservar(int idHabitacion)
        {
            if (idHabitacion <= 0)
            {
                return RedirectToAction("ListarHabitacionesDisponibles");
            }

            HabitacionReservaDto habitacion =
                _reservacionesLN.BuscarHabitacionPorId(idHabitacion);

            if (habitacion == null)
            {
                return HttpNotFound("La habitación solicitada no existe.");
            }

            ReservacionDto modelo = new ReservacionDto
            {
                IdHabitacion = habitacion.Id,
                CodigoDeHabitacion = habitacion.CodigoDeHabitacion,
                NombreDeHabitacion = habitacion.NombreDeHabitacion,
                TipoDeHabitacion = habitacion.TipoDeHabitacion,
                CantidadDeHuespedesPermitidos =
                    habitacion.CantidadDeHuespedesPermitidos
            };

            ViewBag.CostoDeReserva = habitacion.CostoDeReserva;
            ViewBag.CostoDeLimpieza = habitacion.CostoDeLimpieza;

            return View(modelo);
        }

        // Procesa el formulario de reservación
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reservar(ReservacionDto reservacion)
        {
            if (reservacion == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            if (!ModelState.IsValid)
            {
                CargarDatosHabitacion(reservacion);
                return View(reservacion);
            }

            try
            {
                _reservacionesLN.Registrar(reservacion);

                TempData["Mensaje"] =
                    "La reservación se registró correctamente.";

                return RedirectToAction(
                    "ListarHabitacionesDisponibles");
            }
            catch (Exception ex)
            {
                // Registrar el error técnico completo en Visual Studio
                Trace.TraceError(
                    "Error al registrar la reservación: {0}",
                    ex.ToString());

                Debug.WriteLine(
                    "ERROR AL REGISTRAR RESERVACIÓN:");
                Debug.WriteLine(ex.ToString());

                // Mostrar la causa más profunda del error
                Exception errorReal = ex.GetBaseException();

                ViewBag.Error =
                    "No se pudo guardar la reservación. Detalle: "
                    + errorReal.Message;

                try
                {
                    CargarDatosHabitacion(reservacion);
                }
                catch (Exception errorCarga)
                {
                    Trace.TraceError(
                        "Error al recuperar los datos de la habitación: {0}",
                        errorCarga.ToString());
                }

                return View(reservacion);
            }
        }

        // Recupera los datos de la habitación para mostrar el formulario
        private void CargarDatosHabitacion(ReservacionDto reservacion)
        {
            if (reservacion == null || reservacion.IdHabitacion <= 0)
            {
                return;
            }

            HabitacionReservaDto habitacion =
                _reservacionesLN.BuscarHabitacionPorId(
                    reservacion.IdHabitacion);

            if (habitacion == null)
            {
                return;
            }

            ViewBag.CostoDeReserva = habitacion.CostoDeReserva;
            ViewBag.CostoDeLimpieza = habitacion.CostoDeLimpieza;

            reservacion.CodigoDeHabitacion =
                habitacion.CodigoDeHabitacion;

            reservacion.NombreDeHabitacion =
                habitacion.NombreDeHabitacion;

            reservacion.TipoDeHabitacion =
                habitacion.TipoDeHabitacion;

            reservacion.CantidadDeHuespedesPermitidos =
                habitacion.CantidadDeHuespedesPermitidos;
        }

        // Muestra el formulario para buscar una reservación
        [HttpGet]
        public ActionResult BuscarReserva()
        {
            return View();
        }

        // Busca una reservación por su identificador
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuscarReserva(int idReservacion)
        {
            if (idReservacion <= 0)
            {
                TempData["Mensaje"] =
                    "No se encontró la reservación. Verifique el número e inténtelo nuevamente.";

                return RedirectToAction(
                    "ListarHabitacionesDisponibles");
            }

            ReservacionDto reservacion =
                _reservacionesLN.BuscarPorId(idReservacion);

            if (reservacion == null)
            {
                TempData["Mensaje"] =
                    "No se encontró la reservación. Verifique el número e inténtelo nuevamente.";

                return RedirectToAction(
                    "ListarHabitacionesDisponibles");
            }

            return View("DetallesReservacion", reservacion);
        }

        // Muestra los detalles de una reservación
        [HttpGet]
        public ActionResult DetallesReservacion(int id)
        {
            if (id <= 0)
            {
                TempData["Mensaje"] =
                    "No se encontró la reservación solicitada.";

                return RedirectToAction(
                    "ListarHabitacionesDisponibles");
            }

            ReservacionDto reservacion =
                _reservacionesLN.BuscarPorId(id);

            if (reservacion == null)
            {
                TempData["Mensaje"] =
                    "No se encontró la reservación solicitada.";

                return RedirectToAction(
                    "ListarHabitacionesDisponibles");
            }

            return View(reservacion);
        }

        // Lista todas las reservaciones o filtra por habitación
        [HttpGet]
        public ActionResult ListarReservas(int? idHabitacion)
        {
            List<ReservacionDto> reservas;

            if (idHabitacion.HasValue && idHabitacion.Value > 0)
            {
                reservas = _reservacionesLN.ObtenerPorHabitacion(
                    idHabitacion.Value);
            }
            else
            {
                reservas = _reservacionesLN.ObtenerTodas();
            }

            ViewBag.FiltroHabitacion = idHabitacion;

            return View(reservas);
        }
    }
}
