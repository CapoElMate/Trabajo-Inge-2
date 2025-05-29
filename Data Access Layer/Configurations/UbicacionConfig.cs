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
    public class UbicacionConfig: IEntityTypeConfiguration<Ubicacion>
    {
        public void Configure(EntityTypeBuilder<Ubicacion> builder)
        {
            builder.HasKey(x => x.UbicacionName);
            builder.Property(x => x.UbicacionName).HasColumnName("UbicacionName").IsRequired();
        }
    }
}
