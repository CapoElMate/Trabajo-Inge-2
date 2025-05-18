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
    public class Empleado_MaquinaConfig: IEntityTypeConfiguration<Empleado_Maquina>
    {
        public void Configure(EntityTypeBuilder<Empleado_Maquina> builder)
        {
            builder.HasKey(x => new { x.NroEmpleado, x.IdMaquina});

            builder.HasOne(x => x.Empleado)
                .WithMany(e => e.Empleado_Maquinas)
                .HasForeignKey(x => x.NroEmpleado);
            builder.HasOne(x => x.Maquina)
                .WithMany(m => m.Empleado_Maquinas)
                .HasForeignKey(x => x.IdMaquina);
        }
    }
}
