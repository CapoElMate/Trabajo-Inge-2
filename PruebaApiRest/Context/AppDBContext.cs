using Microsoft.EntityFrameworkCore;
using PruebaApiRest.Models;

namespace PruebaApiRest.Context
{
    public class AppDBContext: DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            
        }

        public DbSet<Persona> Personas { get; set; } 

    }
}
