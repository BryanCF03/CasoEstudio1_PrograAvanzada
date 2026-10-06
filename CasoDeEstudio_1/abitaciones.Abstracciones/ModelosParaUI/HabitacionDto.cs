using System;

namespace Habitaciones.Abstracciones.ModelosParaUI
{
    public class HabitacionDto
    {
        public int Id { get; set; }
        public string CodigoDeHabitacion { get; set; }
        public string NombreDeHabitacion { get; set; }
        public int CantidadDeHuespedesPermitidos { get; set; }
        public int CantidadDeCamas { get; set; }
        public int CantidadDeBanos { get; set; }
        public string Ubicacion { get; set; }
        public string EncargadoDeLimpieza { get; set; }
        public int TipoDeHabitacion { get; set; }
        public decimal CostoDeLimpieza { get; set; }
        public decimal CostoDeReserva { get; set; }
        public DateTime FechaDeRegistro { get; set; }
        public DateTime? FechaDeModificacion { get; set; }
        public bool Estado { get; set; }

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