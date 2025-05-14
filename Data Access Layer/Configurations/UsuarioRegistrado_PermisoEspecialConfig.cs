using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Configurations
{
    public class UsuarioRegistrado_PermisosEspecialesConfiguration : IEntityTypeConfiguration<UsuarioRegistrado_PermisoEspecial>
    {
        public void Configure(EntityTypeBuilder<UsuarioRegistrado_PermisoEspecial> builder)
        {
            builder.HasKey(x => new { x.UsuarioRegistradoDNI, x.Permiso });

            builder.HasOne(x => x.UsuarioRegistrado)
                .WithMany(u => u.PermisosEspeciales)
                .HasForeignKey(x => x.UsuarioRegistradoDNI);

            builder.HasOne(x => x.PermisoEspecial)
                .WithMany(p => p.UsuariosRegistrados)
                .HasForeignKey(x => x.Permiso);
        }
    }
}
