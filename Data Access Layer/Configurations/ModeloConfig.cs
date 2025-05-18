using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.Configurations
{
    public class ModeloConfig: IEntityTypeConfiguration<Modelo>
    {
        public void Configure(EntityTypeBuilder<Modelo> builder)
        {
            builder.HasKey(m => new { m.ModeloName, m.MarcaName});
            builder.Property(m => m.ModeloName)
                .HasMaxLength(50);

            builder.HasOne(marca => marca.Marca)           
                    .WithMany(modelos => modelos.Modelos)         
                    .HasForeignKey(modelo => modelo.MarcaName); 
        }
    }
}
