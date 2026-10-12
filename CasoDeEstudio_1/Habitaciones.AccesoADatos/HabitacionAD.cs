using System;
using System.Collections.Generic;
using System.Linq;
using Habitaciones.AccesoADatos.Contexto;
using Habitaciones.AccesoADatos.Entidades;

namespace Habitaciones.AccesoADatos
{
    public class HabitacionAD
    {
        public List<Habitacion> Listar()
        {
            using (var contexto = new HabitacionesContext())
            {
                return contexto.Habitaciones
                    .OrderBy(h => h.CodigoDeHabitacion)
                    .ToList();
            }
        }

        public bool Registrar(Habitacion habitacion)
        {
            if (habitacion == null)
            {
                throw new ArgumentNullException(nameof(habitacion));
            }

            using (var contexto = new HabitacionesContext())
            {
                contexto.Habitaciones.Add(habitacion);
                return contexto.SaveChanges() > 0;
            }
        }
    }
}