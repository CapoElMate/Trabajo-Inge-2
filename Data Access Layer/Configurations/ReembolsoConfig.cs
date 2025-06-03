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
    public class ReembolsoConfig : IEntityTypeConfiguration<Reembolso>
    {
        public void Configure(EntityTypeBuilder<Reembolso> builder)
        {
            builder.HasKey(r => r.idReembolso);

            builder.Property(r => r.Motivo)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(r => r.Status)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(r => r.Monto)
                .IsRequired()
                .HasPrecision(18, 2);

            // Relación con Cliente (muchos Reembolsos por Cliente)
            builder.HasOne(r => r.Cliente)
                .WithMany(c => c.Reembolsos)
                .HasForeignKey(r => r.DNICliente)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación con Alquiler (un Reembolso por Alquiler, un Alquiler tiene un Reembolso)
            builder.HasOne(r => r.Alquiler)
                .WithOne(a => a.Reembolso)
                .HasForeignKey<Reembolso>(r => r.idAlquiler)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
