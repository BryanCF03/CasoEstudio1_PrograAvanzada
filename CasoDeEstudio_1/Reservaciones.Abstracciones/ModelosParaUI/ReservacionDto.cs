using System;

namespace Reservaciones.Abstracciones.ModelosParaUI
{
    public class ReservacionDto
    {
        public int Id { get; set; }
        public string NombreDeLaPersona { get; set; }
        public string Identificacion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public decimal MontoTotal { get; set; }
        public DateTime FechaInicioReserva { get; set; }
        public DateTime FechaFinReserva { get; set; }
        public DateTime FechaDeRegistro { get; set; }
        public int IdHabitacion { get; set; }

        public string CodigoDeHabitacion { get; set; }
        public string NombreDeHabitacion { get; set; }
        public int TipoDeHabitacion { get; set; }
        public int CantidadDeHuespedesPermitidos { get; set; }

        public string TipoDeHabitacionEnProsa
        {
            get
            {
                switch (TipoDeHabitacion)
                {
                    case 1: return "Junior";
                    case 2: return "Superior";
                    case 3: return "Suite";
                    default: return "Desconocido";
                }
            }
        }
    }
}