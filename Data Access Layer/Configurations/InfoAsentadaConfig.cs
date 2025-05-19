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
    public class InfoAsentadaConfig : IEntityTypeConfiguration<InfoAsentada>
    {
        public void Configure(EntityTypeBuilder<InfoAsentada> builder)
        {
            builder.HasKey(i => new { i.idInfo, i.idAlquiler });

            builder.Property(i => i.fec)
                .IsRequired();

            builder.Property(i => i.Contenido)
                .IsRequired();

            // Relación con Alquiler (muchos InfoAsentada a uno Alquiler)
            builder.HasOne(i => i.Alquiler)
                .WithMany(a => a.InfoAsentada)
                .HasForeignKey(i => i.idAlquiler)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación con Empleado (muchos InfoAsentada a uno Empleado)
            builder.HasOne(i => i.Empleado)
                .WithMany(e => e.InfoAsentada)
                .HasForeignKey(i => i.DNIEmpleado)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
