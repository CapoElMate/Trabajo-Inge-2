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
    public class RespuestaConfig: IEntityTypeConfiguration<Respuesta>
    {
        public void Configure(EntityTypeBuilder<Respuesta> builder)
        {
            builder.HasKey(r => new { r.idRespuesta});
            builder.Property(r => r.Contenido).IsRequired().HasMaxLength(150);
            builder.Property(r => r.fec).IsRequired();
            builder.Property(r => r.isDeleted).HasDefaultValue(false).IsRequired();
        }
    }
}
