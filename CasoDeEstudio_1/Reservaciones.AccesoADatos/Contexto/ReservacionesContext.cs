using Habitaciones.AccesoADatos.Entidades;
using Reservaciones.AccesoADatos.Entidades;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;

namespace Reservaciones.AccesoADatos.Contexto
{
    public class ReservacionesContext : DbContext
    {
        public ReservacionesContext()
            : base("ReservacionesConnection")
        {
        }

        public DbSet<Reservacion> Reservaciones { get; set; }

        public DbSet<Habitacion> Habitaciones { get; set; }
    }
}