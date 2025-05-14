using System.Reflection;
using Domain_Layer.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer
{
    public class ApplicationDbContext : DbContext
    {
        //Add-Migration InitialCreate -Project "Data Access Layer" -StartupProject "API Layer" -OutputDir "Data Access Layer/Migrations"
        //update-database -Project "Data Access Layer" -StartupProject "API Layer"
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

        public DbSet<UsuarioRegistrado> UsuariosRegistrados => Set<UsuarioRegistrado>();
        public DbSet<Cliente> Cliente => Set<Cliente>();
        public DbSet<Empleado> Empleado => Set<Empleado>();
        public DbSet<Rol> Rol => Set<Rol>();
        public DbSet<Permiso> Permiso => Set<Permiso>();
        public DbSet<PermisoEspecial> PermisoEspeciales => Set<PermisoEspecial>();
    }
}
