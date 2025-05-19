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
    public class ArchivoConfig: IEntityTypeConfiguration<Archivo>
    {
        public void Configure(EntityTypeBuilder<Archivo> builder)
        {
            builder.HasKey(a => new { a.idArchivo, a.EntidadID, a.TipoEntidad });
            builder.Property(a => a.EntidadID)
                .IsRequired();
            builder.Property(a => a.TipoEntidad)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(a => a.Nombre)
                .IsRequired()
                .HasMaxLength(120);
            builder.Property(a => a.Descripcion)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(a => a.TipoContenido)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(a => a.Ruta)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(a => a.TipoEntidad).HasConversion<string>();
        }
    }
}
