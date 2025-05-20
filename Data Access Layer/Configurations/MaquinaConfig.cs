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
    public class MaquinaConfig : IEntityTypeConfiguration<Maquina>
    {
        public void Configure(EntityTypeBuilder<Maquina> builder)
        {
            builder.HasKey(x => x.idMaquina);

            builder.Property(x => x.status)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.anioFabricacion)
                .IsRequired();

            // Relación con Marca (muchas Maquinas a una Marca)
            builder.HasOne(x => x.Marca)
                .WithMany()
                .HasForeignKey(x => x.MarcaName)
                .IsRequired();

            // Relación con TipoMaquina (muchas Maquinas a un TipoMaquina)
            builder.HasOne(x => x.TipoMaquina)
                .WithMany(t => t.Maquinas)
                .HasForeignKey(x => x.Tipo)
                .IsRequired();

        }
    }
}
