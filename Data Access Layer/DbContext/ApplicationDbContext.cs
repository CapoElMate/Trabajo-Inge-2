using System.Reflection;
using Domain_Layer.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer
{
    public class ApplicationDbContext : DbContext
    {
        //Add-Migration InitialCreate -Project "Data Access Layer" -StartupProject "API Layer" -OutputDir "Data Access Layer/Migrations"
        //update-database -Project "Data Access Layer" -StartupProject "API Layer"

        /*
         Entidades con ID Compuesto:
            - Reembolso .
            - InfoAsentada .
            - Comentario .
            - Respuesta .
            - Archivo .
            - Recargo .
            - Marca .
          A desarrollar en este sprint:
            - Usurio Registrado, Cliente, Empleado, Rol, Permisos -> Ready
            - Reserva, Pago, TipoEntrega -> Ready
            - Maquina, Empleado_Maquina,PermisosEspeciales, Marca, Modelo, TagMaquina, TipoMaquina -> Ready
            - Publicacion, Ubicacion, Archivos, PoliticaDeCancelación, TagPublicacion -> Ready
            - Alquiler, Reembolso -> Ready
         */
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Alquiler> Alquiler => Set<Alquiler>();
        public DbSet<Archivo> Archivo => Set<Archivo>();
        public DbSet<Cliente> Cliente => Set<Cliente>();
        public DbSet<Comentario> Comentario => Set<Comentario>();
        public DbSet<Devolucion> Devolucion => Set<Devolucion>();
        public DbSet<Empleado> Empleado => Set<Empleado>();
        public DbSet<Empleado_Maquina> Empleado_Maquina => Set<Empleado_Maquina>();
        public DbSet<InfoAsentada> InfoAsentada => Set<InfoAsentada>();
        public DbSet<Maquina> Maquina => Set<Maquina>();
        public DbSet<Marca> Marca => Set<Marca>();
        public DbSet<Modelo> Modelo => Set<Modelo>();
        public DbSet<Pago> Pago => Set<Pago>();
        public DbSet<Permiso> Permiso => Set<Permiso>();
        public DbSet<PermisoEspecial> PermisoEspecial => Set<PermisoEspecial>();
        public DbSet<PoliticaDeCancelacion> PoliticaDeCancelacion => Set<PoliticaDeCancelacion>();
        public DbSet<Publicacion> Publicacion => Set<Publicacion>();
        public DbSet<Recargo> Recargo => Set<Recargo>();
        public DbSet<Reembolso> Reembolso => Set<Reembolso>();
        public DbSet<Reserva> Reserva => Set<Reserva>();
        public DbSet<Respuesta> Respuesta => Set<Respuesta>();
        public DbSet<Rol> Rol => Set<Rol>();
        public DbSet<TagMaquina> TagMaquina => Set<TagMaquina>();
        public DbSet<TagPublicacion> TagPublicacion => Set<TagPublicacion>();
        public DbSet<TipoEntrega> TipoEntrega => Set<TipoEntrega>();
        public DbSet<TipoMaquina> TipoMaquina => Set<TipoMaquina>();
        public DbSet<Ubicacion> Ubicacion => Set<Ubicacion>();
        public DbSet<UsuarioRegistrado> UsuariosRegistrados => Set<UsuarioRegistrado>();
        public DbSet<UsuarioRegistrado_PermisoEspecial> UsuarioRegistrado_PermisoEspecial => Set<UsuarioRegistrado_PermisoEspecial>();
    }
}
