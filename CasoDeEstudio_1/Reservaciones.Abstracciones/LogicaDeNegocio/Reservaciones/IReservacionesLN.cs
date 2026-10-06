using Reservaciones.Abstracciones.ModelosParaUI;
using System.Collections.Generic;

namespace Reservaciones.Abstracciones.LogicaDeNegocio.Reservaciones
{
    public interface IReservacionesLN
    {
        List<HabitacionReservaDto> ObtenerHabitacionesDisponibles();
        HabitacionReservaDto BuscarHabitacionPorId(int idHabitacion);
        void Registrar(ReservacionDto reservacion);

        ReservacionDto BuscarPorId(int id);
        List<ReservacionDto> ObtenerTodas();
        List<ReservacionDto> ObtenerPorHabitacion(int idHabitacion);
    }
}