using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reservaciones.AccesoADatos.Entidades
{
    [Table("RESERVACIONES")]
    public class Reservacion
    {
        [Key]
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
    }
}