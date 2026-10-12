
using System;
using System.Collections.Generic;
using System.Linq;
using Habitaciones.AccesoADatos;
using Habitaciones.AccesoADatos.Entidades;
using Habitaciones.LogicaDeNegocio.DTO;

namespace Habitaciones.LogicaDeNegocio
{
    public class HabitacionLN
    {
        private readonly HabitacionAD habitacionAD = new HabitacionAD();

        public List<HabitacionDTO> Listar()
        {
            return habitacionAD.Listar()
                .Select(h => new HabitacionDTO
                {
                    Id = h.Id,
                    CodigoDeHabitacion = h.CodigoDeHabitacion,
                    NombreDeHabitacion = h.NombreDeHabitacion,
                    CantidadDeHuespedesPermitidos = h.CantidadDeHuespedesPermitidos,
                    CantidadDeCamas = h.CantidadDeCamas,
                    CantidadDeBanos = h.CantidadDeBanos,
                    Ubicacion = h.Ubicacion,
                    EncargadoDeLimpieza = h.EncargadoDeLimpieza,
                    TipoDeHabitacion = h.TipoDeHabitacion,
                    CostoDeLimpieza = h.CostoDeLimpieza,
                    CostoDeReserva = h.CostoDeReserva,
                    FechaDeRegistro = h.FechaDeRegistro,
                    FechaDeModificacion = h.FechaDeModificacion,
                    Estado = h.Estado
                })
                .ToList();
        }

        public bool Registrar(HabitacionDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            // Validar código
            if (string.IsNullOrWhiteSpace(dto.CodigoDeHabitacion))
            {
                throw new ArgumentException(
                    "El código de habitación es obligatorio.");
            }

            if (dto.CodigoDeHabitacion.Length > 7)
            {
                throw new ArgumentException(
                    "El código no puede superar los 7 caracteres.");
            }

            // Validar nombre
            if (string.IsNullOrWhiteSpace(dto.NombreDeHabitacion))
            {
                throw new ArgumentException(
                    "El nombre de habitación es obligatorio.");
            }

            if (dto.NombreDeHabitacion.Length > 30)
            {
                throw new ArgumentException(
                    "El nombre no puede superar los 30 caracteres.");
            }

            // Validar huéspedes, camas y baños
            if (dto.CantidadDeHuespedesPermitidos <= 0)
            {
                throw new ArgumentException(
                    "La cantidad de huéspedes debe ser mayor que cero.");
            }

            if (dto.CantidadDeCamas <= 0)
            {
                throw new ArgumentException(
                    "La cantidad de camas debe ser mayor que cero.");
            }

            if (dto.CantidadDeBanos < 0)
            {
                throw new ArgumentException(
                    "La cantidad de baños no puede ser negativa.");
            }

            // Validar ubicación
            if (string.IsNullOrWhiteSpace(dto.Ubicacion))
            {
                throw new ArgumentException(
                    "La ubicación de la habitación es obligatoria.");
            }

            if (dto.Ubicacion.Length > 10)
            {
                throw new ArgumentException(
                    "La ubicación no puede superar los 10 caracteres.");
            }

            // Validar encargado de limpieza
            if (string.IsNullOrWhiteSpace(dto.EncargadoDeLimpieza))
            {
                throw new ArgumentException(
                    "El encargado de limpieza es obligatorio.");
            }

            if (dto.EncargadoDeLimpieza.Length > 100)
            {
                throw new ArgumentException(
                    "El encargado no puede superar los 100 caracteres.");
            }

            // Validar tipo de habitación
            if (dto.TipoDeHabitacion < 1 || dto.TipoDeHabitacion > 3)
            {
                throw new ArgumentException(
                    "El tipo debe ser 1 (Junior), 2 (Superior) o 3 (Suite).");
            }

            // Validar costos
            if (dto.CostoDeLimpieza <= 0)
            {
                throw new ArgumentException(
                    "El costo de limpieza debe ser mayor que cero.");
            }

            if (dto.CostoDeReserva <= 0)
            {
                throw new ArgumentException(
                    "El costo de reserva debe ser mayor que cero.");
            }

            // Crear entidad para la capa de acceso a datos
            var habitacion = new Habitacion
            {
                CodigoDeHabitacion = dto.CodigoDeHabitacion,
                NombreDeHabitacion = dto.NombreDeHabitacion,
                CantidadDeHuespedesPermitidos =
                    dto.CantidadDeHuespedesPermitidos,
                CantidadDeCamas = dto.CantidadDeCamas,
                CantidadDeBanos = dto.CantidadDeBanos,
                Ubicacion = dto.Ubicacion,
                EncargadoDeLimpieza = dto.EncargadoDeLimpieza,
                TipoDeHabitacion = dto.TipoDeHabitacion,
                CostoDeLimpieza = dto.CostoDeLimpieza,
                CostoDeReserva = dto.CostoDeReserva,
                FechaDeRegistro = DateTime.Now,
                Estado = true
            };

            return habitacionAD.Registrar(habitacion);
        }
    }
}
