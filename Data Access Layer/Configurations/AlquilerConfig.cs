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
    public class AlquilerConfig : IEntityTypeConfiguration<Alquiler>
    {
        public void Configure(EntityTypeBuilder<Alquiler> builder)
        {
            builder.HasKey(a => a.idAlquiler);

            builder.Property(a => a.fecEfectivizacion).IsRequired();
            builder.Property(a => a.isDeleted).HasDefaultValue(false);

            // Relación uno a muchos: Cliente - Alquiler
            builder.HasOne(a => a.Cliente)
                .WithMany(c => c.Alquileres)
                .HasForeignKey(a => a.DNICliente)
                .IsRequired();

            // Relación uno a muchos: Empleado - Alquiler
            builder.HasOne(a => a.Empleado)
                .WithMany(e => e.Alquileres)
                .HasForeignKey(a => a.nroEmpleado)
                .IsRequired();

            // Relación uno a muchos: Reserva - Alquiler (1:1)
            builder.HasOne(a => a.Reserva)
                .WithOne(r => r.Alquiler)
                .HasForeignKey<Alquiler>(a => a.idReserva)
                .IsRequired();
        }
    }
}
