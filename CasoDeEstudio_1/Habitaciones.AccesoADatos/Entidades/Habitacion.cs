using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Habitaciones.AccesoADatos.Entidades
{
    [Table("HABITACIONES")]
    public class Habitacion
    {
        [Key]
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
    }
}