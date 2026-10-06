using System.Data.Entity;
using Habitaciones.AccesoADatos.Entidades;

namespace Habitaciones.AccesoADatos.Contexto
{
    public class HabitacionesContext : DbContext
    {
        public HabitacionesContext()
            : base("HabitacionesConnection")
        {
        }

        public DbSet<Habitacion> Habitaciones { get; set; }
    }
}