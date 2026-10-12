
using System;
using System.ComponentModel.DataAnnotations;

namespace Habitaciones.LogicaDeNegocio.DTO
{
    public class HabitacionDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El código de habitación es obligatorio.")]
        [StringLength(7, ErrorMessage = "El código no puede superar los 7 caracteres.")]
        public string CodigoDeHabitacion { get; set; }

        [Required(ErrorMessage = "El nombre de habitación es obligatorio.")]
        [StringLength(30, ErrorMessage = "El nombre no puede superar los 30 caracteres.")]
        public string NombreDeHabitacion { get; set; }

        [Range(1, 100, ErrorMessage = "La cantidad de huéspedes debe ser mayor que cero.")]
        public int CantidadDeHuespedesPermitidos { get; set; }

        [Range(1, 100, ErrorMessage = "La cantidad de camas debe ser mayor que cero.")]
        public int CantidadDeCamas { get; set; }

        [Range(0, 100, ErrorMessage = "La cantidad de baños no puede ser negativa.")]
        public int CantidadDeBanos { get; set; }

        [Required(ErrorMessage = "La ubicación es obligatoria.")]
        [StringLength(10, ErrorMessage = "La ubicación no puede superar los 10 caracteres.")]
        public string Ubicacion { get; set; }

        [Required(ErrorMessage = "El encargado de limpieza es obligatorio.")]
        [StringLength(100, ErrorMessage = "El encargado no puede superar los 100 caracteres.")]
        public string EncargadoDeLimpieza { get; set; }

        [Range(1, 3, ErrorMessage = "Seleccione Junior, Superior o Suite.")]
        public int TipoDeHabitacion { get; set; }

        [Range(typeof(decimal), "0,01", "999999999",
    ErrorMessage = "El costo de limpieza debe ser mayor que cero.")]
        public decimal CostoDeLimpieza { get; set; }

        [Range(typeof(decimal), "0,01", "999999999",
            ErrorMessage = "El costo de reserva debe ser mayor que cero.")]
        public decimal CostoDeReserva { get; set; }

        public DateTime FechaDeRegistro { get; set; }

        public DateTime? FechaDeModificacion { get; set; }

        public bool Estado { get; set; }
    }
}
