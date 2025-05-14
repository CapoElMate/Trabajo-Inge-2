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
    public class PermisoEspecialesConfig : IEntityTypeConfiguration<PermisoEspecial>
    {
        public void Configure(EntityTypeBuilder<PermisoEspecial> builder)
        {
            builder.HasKey(permisosE => permisosE.Permiso);
            builder.Property(permisosE => permisosE.Permiso).HasMaxLength(50);
        }
    }
}
