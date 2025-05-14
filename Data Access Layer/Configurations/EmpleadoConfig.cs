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
    public class EmpleadoConfig : IEntityTypeConfiguration<Empleado>
    {
        public void Configure(EntityTypeBuilder<Empleado> builder)
        {
            builder.HasKey(empleado => empleado.DNI);
            builder.Property(empleado => empleado.DNI)
                .IsRequired();
            builder.HasIndex(empleado => empleado.DNI)
                .IsUnique();

            builder.Property(empleado => empleado.nroEmpleado)
                .IsRequired();
            builder.HasIndex(empleado => empleado.nroEmpleado)
                .IsUnique();
        }
    }
}
