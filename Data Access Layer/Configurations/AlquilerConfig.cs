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
    public class AlquilerConfig: IEntityTypeConfiguration<Alquiler>
    {
        public void Configure(EntityTypeBuilder<Alquiler> builder)
        {
            builder.HasKey(a => a.idAlquiler);
            builder.Property(a => a.fecEfectivizacion).IsRequired();
            builder.Property(a => a.isDeleted).HasDefaultValue(false);
            builder.Property(builder => builder.Cliente).IsRequired();
            builder.Property(a => a.Empleado).IsRequired();
            builder.Property(a => a.Reserva).IsRequired();
        }
    }
}
