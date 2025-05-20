using System.Reflection;
using Domain_Layer.Entidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer
{

    public class ApplicationDbContext : IdentityDbContext<UsuarioRegistrado> //hago que herede de Ident.DbContext para que banque el identity
    {
        //Add-Migration InitialCreate -Project "Data Access Layer" -StartupProject "API Layer" -OutputDir "Migrations"
        //update-database -Project "Data Access Layer" -StartupProject "API Layer"

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            modelBuilder.Entity<UsuarioRegistrado>(b =>
            {
                b.HasKey(u => u.DNI);
                b.Property(u => u.DNI).ValueGeneratedNever();
                b.Ignore(u => u.Id); // OJO: Esto puede romper Identity
            });
        }

        public DbSet<Alquiler> Alquileres => Set<Alquiler>();
        public DbSet<Archivo> Archivos => Set<Archivo>();
        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<Comentario> Comentarios => Set<Comentario>();
        public DbSet<Devolucion> Devoluciones => Set<Devolucion>();
        public DbSet<Empleado> Empleados => Set<Empleado>();
        public DbSet<Empleado_Maquina> Empleado_Maquina => Set<Empleado_Maquina>();
        public DbSet<InfoAsentada> InfoAsentada => Set<InfoAsentada>();
        public DbSet<Maquina> Maquinas => Set<Maquina>();
        public DbSet<Marca> Marcas => Set<Marca>();
        public DbSet<Modelo> Modelos => Set<Modelo>();
        public DbSet<Pago> Pagos => Set<Pago>();
        //public DbSet<Permiso> Permisos => Set<Permiso>();
        public DbSet<PermisoEspecial> PermisosEspeciales => Set<PermisoEspecial>();
        public DbSet<PoliticaDeCancelacion> PoliticasDeCancelacion => Set<PoliticaDeCancelacion>();
        public DbSet<Publicacion> Publicaciones => Set<Publicacion>();
        public DbSet<Recargo> Recargos => Set<Recargo>();
        public DbSet<Reembolso> Reembolsos => Set<Reembolso>();
        public DbSet<Reserva> Reservas => Set<Reserva>();
        public DbSet<Respuesta> Respuestas => Set<Respuesta>();
        //public DbSet<Rol> Roles => Set<Rol>();
        public DbSet<TagMaquina> TagsMaquina => Set<TagMaquina>();
        public DbSet<TagPublicacion> TagsPublicacion => Set<TagPublicacion>();
        public DbSet<TipoEntrega> TiposEntrega => Set<TipoEntrega>();
        public DbSet<TipoMaquina> TiposMaquina => Set<TipoMaquina>();
        public DbSet<Ubicacion> Ubicaciones => Set<Ubicacion>();
        public DbSet<UsuarioRegistrado> UsuariosRegistrados => Set<UsuarioRegistrado>();
        public DbSet<UsuarioRegistrado_PermisoEspecial> UsuarioRegistrado_PermisoEspecial => Set<UsuarioRegistrado_PermisoEspecial>();
    }
}
