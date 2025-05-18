using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.Configurations
{
    public class DevolucionConfig : IEntityTypeConfiguration<Devolucion>
    {
        public void Configure(EntityTypeBuilder<Devolucion> builder)
        {
            builder.HasKey(d => d.idDevolucion);

            builder.Property(d => d.fecDevolucion).IsRequired();
            builder.Property(d => d.Descripcion).IsRequired().HasMaxLength(500);

            // Relación muchos a uno: Devolucion -> Empleado
            builder.HasOne(d => d.Empleado)
                   .WithMany(e => e.Devoluciones)
                   .HasForeignKey(d => d.DNIEmpleado)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relación muchos a uno: Devolucion -> Ubicacion
            builder.HasOne(d => d.Ubicacion)
                   .WithMany(u => u.Devoluciones)
                   .HasForeignKey(d => d.UbicacionName)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relación uno a uno: Devolucion -> Alquiler
            builder.HasOne(d => d.Alquiler)
                   .WithOne(a => a.Devolucion)
                   .HasForeignKey<Devolucion>(d => d.idAlquiler)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
