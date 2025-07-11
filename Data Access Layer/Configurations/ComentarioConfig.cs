﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.Configurations
{
    public class ComentarioConfig : IEntityTypeConfiguration<Comentario>
    {
        public void Configure(EntityTypeBuilder<Comentario> builder)
        {
            builder.HasKey(c => new { c.idComentario, c.idPublicacion });

            builder.Property(c => c.Contenido)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.fec)
                .IsRequired();

            builder.Property(c => c.isDeleted)
                .HasDefaultValue(false)
                .IsRequired();

            // Relación Comentario -> Respuesta (1:1, opcional)
            builder.HasOne(c => c.Respuesta)
                .WithOne(r => r.Comentario)
                .HasForeignKey<Comentario>(c => c.idRespuesta)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Comentario -> Publicacion (N:1)
            builder.HasOne(c => c.Publicacion)
                .WithMany(p => p.Comentarios)
                .HasForeignKey(c => c.idPublicacion)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación Comentario -> Cliente (N:1)
            builder.HasOne(c => c.Cliente)
                .WithMany(cl => cl.Comentarios)
                .HasForeignKey(c => c.DNICliente)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
