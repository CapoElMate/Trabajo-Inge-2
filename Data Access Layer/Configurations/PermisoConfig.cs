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
    public class PermisoConfig: IEntityTypeConfiguration<Permiso>
    {
        public void Configure(EntityTypeBuilder<Permiso> builder)
        {
            builder.HasKey(p => p.idPermiso);
            builder.Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(p => p.Descripcion)
                .IsRequired()
                .HasMaxLength(150);
        }
    }
}
