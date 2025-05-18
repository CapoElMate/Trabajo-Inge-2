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
    public class PublicacionConfig: IEntityTypeConfiguration<Publicacion>
    {
        public void Configure(EntityTypeBuilder<Publicacion> builder)
        {
            builder.HasKey(p => p.idPublicacion);
            builder.Property(p => p.Descripcion).IsRequired();
            builder.Property(p => p.PrecioPorDia).IsRequired();
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.Ubicacion).IsRequired();
            builder.Property(p => p.PoliticaDeCancelacion).IsRequired();
            builder.Property(p => p.Maquina).IsRequired();
        }
    }
}
