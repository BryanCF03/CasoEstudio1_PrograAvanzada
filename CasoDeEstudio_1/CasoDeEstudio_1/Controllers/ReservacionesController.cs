using Reservaciones.Abstracciones.LogicaDeNegocio.Reservaciones;
using Reservaciones.Abstracciones.ModelosParaUI;
using Reservaciones.LogicaDeNegocio.Reservaciones;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Semana1_Framework.Controllers
{
    public class ReservacionesController : Controller
    {
        private IReservacionesLN _reservacionesLN;

        public ReservacionesController()
        {
            _reservacionesLN = new ReservacionesLN();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListarHabitacionesDisponibles()
        {
            List<HabitacionReservaDto> habitaciones =
                _reservacionesLN.ObtenerHabitacionesDisponibles();

            return View(habitaciones);
        }

        [HttpGet]
        public ActionResult Reservar(int idHabitacion)
        {
            HabitacionReservaDto habitacion =
                _reservacionesLN.BuscarHabitacionPorId(idHabitacion);

            if (habitacion == null)
            {
                return HttpNotFound();
            }

            ReservacionDto modelo = new ReservacionDto
            {
                IdHabitacion = habitacion.Id,
                CodigoDeHabitacion = habitacion.CodigoDeHabitacion,
                NombreDeHabitacion = habitacion.NombreDeHabitacion,
                TipoDeHabitacion = habitacion.TipoDeHabitacion,
                CantidadDeHuespedesPermitidos = habitacion.CantidadDeHuespedesPermitidos
            };

            ViewBag.CostoDeReserva = habitacion.CostoDeReserva;
            ViewBag.CostoDeLimpieza = habitacion.CostoDeLimpieza;

            return View(modelo);
        }

        [HttpPost]
        public ActionResult Reservar(ReservacionDto reservacion)
        {
            try
            {
                _reservacionesLN.Registrar(reservacion);

                return RedirectToAction("ListarHabitacionesDisponibles");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;

                HabitacionReservaDto habitacion =
                    _reservacionesLN.BuscarHabitacionPorId(reservacion.IdHabitacion);

                if (habitacion != null)
                {
                    ViewBag.CostoDeReserva = habitacion.CostoDeReserva;
                    ViewBag.CostoDeLimpieza = habitacion.CostoDeLimpieza;
                    reservacion.CodigoDeHabitacion = habitacion.CodigoDeHabitacion;
                    reservacion.NombreDeHabitacion = habitacion.NombreDeHabitacion;
                    reservacion.TipoDeHabitacion = habitacion.TipoDeHabitacion;
                }

                return View(reservacion);
            }
        }

        [HttpGet]
        public ActionResult BuscarReserva()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BuscarReserva(int idReservacion)
        {
            if (idReservacion <= 0)
            {
                TempData["Mensaje"] = "Estimado usuario, no se ha encontrado la reservación, favor realice una";
                return RedirectToAction("ListarHabitacionesDisponibles");
            }

            ReservacionDto reservacion = _reservacionesLN.BuscarPorId(idReservacion);

            if (reservacion == null)
            {
                TempData["Mensaje"] = "Estimado usuario, no se ha encontrado la reservación, favor realice una";
                return RedirectToAction("ListarHabitacionesDisponibles");
            }

            return View("DetallesReservacion", reservacion);
        }

        [HttpGet]
        public ActionResult DetallesReservacion(int id)
        {
            if (id <= 0)
            {
                TempData["Mensaje"] = "Estimado usuario, no se ha encontrado la reservación, favor realice una";
                return RedirectToAction("ListarHabitacionesDisponibles");
            }

            ReservacionDto reservacion = _reservacionesLN.BuscarPorId(id);

            if (reservacion == null)
            {
                TempData["Mensaje"] = "Estimado usuario, no se ha encontrado la reservación, favor realice una";
                return RedirectToAction("ListarHabitacionesDisponibles");
            }

            return View(reservacion);
        }

        [HttpGet]
        public ActionResult ListarReservas(int? idHabitacion)
        {
            List<ReservacionDto> reservas;

            if (idHabitacion.HasValue && idHabitacion.Value > 0)
            {
                reservas = _reservacionesLN.ObtenerPorHabitacion(idHabitacion.Value);
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