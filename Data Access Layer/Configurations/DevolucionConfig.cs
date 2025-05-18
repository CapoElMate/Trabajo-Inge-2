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
    public class DevolucionConfig: IEntityTypeConfiguration<Devolucion>
    {
        public void Configure(EntityTypeBuilder<Devolucion> builder)
        {
            builder.HasKey(d => d.idDevolucion);
            builder.Property(d => d.Ubicacion).IsRequired();
            builder.Property(d => d.fecDevolucion).IsRequired();
            builder.Property(d => d.Descripcion).IsRequired().HasMaxLength(500);
        }
    }
}
