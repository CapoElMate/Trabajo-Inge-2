using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.Configurations
{
    public class TipoMaquinaConfig : IEntityTypeConfiguration<TipoMaquina>
    {
        public void Configure(EntityTypeBuilder<TipoMaquina> builder)
        {
            builder.HasKey(tipo => tipo.Tipo);
            builder.Property(tipo => tipo.Tipo)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(t => t.Maquinas)
                .WithOne();
        }
    }
}
