using Reservaciones.Abstracciones.LogicaDeNegocio.Reservaciones;
using Reservaciones.Abstracciones.ModelosParaUI;
using Reservaciones.AccesoADatos.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reservaciones.LogicaDeNegocio.Reservaciones
{
    public class ReservacionesLN : IReservacionesLN
    {
        public List<HabitacionReservaDto> ObtenerHabitacionesDisponibles()
        {
            using (ReservacionesContext contexto = new ReservacionesContext())
            {
                return contexto.Habitaciones
                    .Where(h => h.Estado)
                    .Select(h => new HabitacionReservaDto
                    {
                        Id = h.Id,
                        CodigoDeHabitacion = h.CodigoDeHabitacion,
                        NombreDeHabitacion = h.NombreDeHabitacion,
                        CantidadDeHuespedesPermitidos = h.CantidadDeHuespedesPermitidos,
                        CantidadDeCamas = h.CantidadDeCamas,
                        CantidadDeBanos = h.CantidadDeBanos,
                        Ubicacion = h.Ubicacion,
                        CostoDeReserva = h.CostoDeReserva,
                        CostoDeLimpieza = h.CostoDeLimpieza,
                        TipoDeHabitacion = h.TipoDeHabitacion
                    })
                    .ToList();
            }
        }

        public HabitacionReservaDto BuscarHabitacionPorId(int idHabitacion)
        {
            using (ReservacionesContext contexto = new ReservacionesContext())
            {
                return contexto.Habitaciones
                    .Where(h => h.Id == idHabitacion)
                    .Select(h => new HabitacionReservaDto
                    {
                        Id = h.Id,
                        CodigoDeHabitacion = h.CodigoDeHabitacion,
                        NombreDeHabitacion = h.NombreDeHabitacion,
                        CantidadDeHuespedesPermitidos = h.CantidadDeHuespedesPermitidos,
                        CantidadDeCamas = h.CantidadDeCamas,
                        CantidadDeBanos = h.CantidadDeBanos,
                        Ubicacion = h.Ubicacion,
                        CostoDeReserva = h.CostoDeReserva,
                        CostoDeLimpieza = h.CostoDeLimpieza,
                        TipoDeHabitacion = h.TipoDeHabitacion
                    })
                    .FirstOrDefault();
            }
        }

        public void Registrar(ReservacionDto reservacion)
        {
            using (ReservacionesContext contexto = new ReservacionesContext())
            {
                var habitacion = contexto.Habitaciones
                    .FirstOrDefault(h => h.Id == reservacion.IdHabitacion);

                if (habitacion == null)
                {
                    throw new Exception("La habitación seleccionada no existe.");
                }

                if (!habitacion.Estado)
                {
                    throw new Exception("La habitación seleccionada no está disponible para reservar.");
                }

                if (reservacion.FechaFinReserva <= reservacion.FechaInicioReserva)
                {
                    throw new Exception("La fecha de fin debe ser posterior a la fecha de inicio.");
                }

                int cantidadDias = (reservacion.FechaFinReserva.Date -
                                    reservacion.FechaInicioReserva.Date).Days;

                if (cantidadDias <= 0)
                {
                    throw new Exception("La reservación debe ser de al menos 1 día.");
                }

                decimal montoTotal =
                    (cantidadDias * habitacion.CostoDeReserva) +
                    habitacion.CostoDeLimpieza;

                var entidad = new global::Reservaciones.AccesoADatos.Entidades.Reservacion
                {
                    NombreDeLaPersona = reservacion.NombreDeLaPersona,
                    Identificacion = reservacion.Identificacion,
                    Telefono = reservacion.Telefono,
                    Correo = reservacion.Correo,
                    FechaNacimiento = reservacion.FechaNacimiento,
                    Direccion = reservacion.Direccion,
                    MontoTotal = montoTotal,
                    FechaInicioReserva = reservacion.FechaInicioReserva,
                    FechaFinReserva = reservacion.FechaFinReserva,
                    FechaDeRegistro = DateTime.Now,
                    IdHabitacion = reservacion.IdHabitacion
                };

                contexto.Reservaciones.Add(entidad);
                contexto.SaveChanges();
            }
        }

        public ReservacionDto BuscarPorId(int id)
        {
            using (ReservacionesContext contexto = new ReservacionesContext())
            {
                var reservacion = contexto.Reservaciones
                    .Where(r => r.Id == id)
                    .Select(r => new ReservacionDto
                    {
                        Id = r.Id,
                        NombreDeLaPersona = r.NombreDeLaPersona,
                        Identificacion = r.Identificacion,
                        Telefono = r.Telefono,
                        Correo = r.Correo,
                        FechaNacimiento = r.FechaNacimiento,
                        Direccion = r.Direccion,
                        MontoTotal = r.MontoTotal,
                        FechaInicioReserva = r.FechaInicioReserva,
                        FechaFinReserva = r.FechaFinReserva,
                        FechaDeRegistro = r.FechaDeRegistro,
                        IdHabitacion = r.IdHabitacion
                    })
                    .FirstOrDefault();

                if (reservacion == null)
                {
                    return null;
                }

                var habitacion = contexto.Habitaciones
                    .Where(h => h.Id == reservacion.IdHabitacion)
                    .Select(h => new
                    {
                        h.CodigoDeHabitacion,
                        h.NombreDeHabitacion,
                        h.TipoDeHabitacion,
                        h.CantidadDeHuespedesPermitidos
                    })
                    .FirstOrDefault();

                if (habitacion != null)
                {
                    reservacion.CodigoDeHabitacion = habitacion.CodigoDeHabitacion;
                    reservacion.NombreDeHabitacion = habitacion.NombreDeHabitacion;
                    reservacion.TipoDeHabitacion = habitacion.TipoDeHabitacion;
                    reservacion.CantidadDeHuespedesPermitidos = habitacion.CantidadDeHuespedesPermitidos;
                }

                return reservacion;
            }
        }

        public List<ReservacionDto> ObtenerTodas()
        {
            using (ReservacionesContext contexto = new ReservacionesContext())
            {
                return contexto.Reservaciones
                    .Select(r => new ReservacionDto
                    {
                        Id = r.Id,
                        NombreDeLaPersona = r.NombreDeLaPersona,
                        Identificacion = r.Identificacion,
                        Telefono = r.Telefono,
                        Correo = r.Correo,
                        FechaNacimiento = r.FechaNacimiento,
                        Direccion = r.Direccion,
                        MontoTotal = r.MontoTotal,
                        FechaInicioReserva = r.FechaInicioReserva,
                        FechaFinReserva = r.FechaFinReserva,
                        FechaDeRegistro = r.FechaDeRegistro,
                        IdHabitacion = r.IdHabitacion
                    })
                    .ToList();
            }
        }

        public List<ReservacionDto> ObtenerPorHabitacion(int idHabitacion)
        {
            using (ReservacionesContext contexto = new ReservacionesContext())
            {
                return contexto.Reservaciones
                    .Where(r => r.IdHabitacion == idHabitacion)
                    .Select(r => new ReservacionDto
                    {
                        Id = r.Id,
                        NombreDeLaPersona = r.NombreDeLaPersona,
                        Identificacion = r.Identificacion,
                        Telefono = r.Telefono,
                        Correo = r.Correo,
                        FechaNacimiento = r.FechaNacimiento,
                        Direccion = r.Direccion,
                        MontoTotal = r.MontoTotal,
                        FechaInicioReserva = r.FechaInicioReserva,
                        FechaFinReserva = r.FechaFinReserva,
                        FechaDeRegistro = r.FechaDeRegistro,
                        IdHabitacion = r.IdHabitacion
                    })
                    .ToList();
            }
        }
    }
}