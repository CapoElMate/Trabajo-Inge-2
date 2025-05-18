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
    public class PublicacionConfig : IEntityTypeConfiguration<Publicacion>
    {
        public void Configure(EntityTypeBuilder<Publicacion> builder)
        {
            builder.HasKey(p => p.idPublicacion);

            builder.Property(p => p.Descripcion).IsRequired();
            builder.Property(p => p.PrecioPorDia).IsRequired();
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.Politica).IsRequired();
            builder.Property(p => p.UbicacionName).IsRequired();

            // Relación con Maquina (muchas publicaciones a una maquina)
            builder.HasOne(p => p.Maquina)
                .WithMany(m => m.Publicaciones)
                .HasForeignKey(p => p.idMaquina)
                .IsRequired();

            // Relación con PoliticaDeCancelacion (muchas publicaciones a una política)
            builder.HasOne(p => p.PoliticaDeCancelacion)
                .WithMany(pc => pc.Publicaciones)
                .HasForeignKey(p => p.Politica)
                .IsRequired();

            // Relación con Ubicacion (muchas publicaciones a una ubicación)
            builder.HasOne(p => p.Ubicacion)
                .WithMany(u => u.Publicaciones)
                .HasForeignKey(p => p.UbicacionName)
                .IsRequired();
        }
    }
}
