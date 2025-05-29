using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain_Layer.Configurations
{
    public class UsuarioRegistradoConfig : IEntityTypeConfiguration<UsuarioRegistrado>
    {
        public void Configure(EntityTypeBuilder<UsuarioRegistrado> builder)
        {
            builder.HasKey(user => user.DNI);
            builder.Property(user => user.DNI)
                .IsRequired();
            builder.HasIndex(user => user.DNI)
                .IsUnique();
            builder.Property(c => c.DNI)
                  .ValueGeneratedNever();

            builder.Property(user => user.Email)
                .IsRequired()
                .HasMaxLength(254);
            builder.HasIndex(user => user.Email)
                .IsUnique();

            builder.Property(user => user.Nombre)
                .IsRequired();
            builder.Property(user => user.Apellido)
                .IsRequired();
            builder.Property(user => user.Edad)
                .IsRequired();

            builder.Property(user => user.Calle)
                .IsRequired()
                .HasMaxLength(70);
            builder.Property(user => user.Altura)
                .IsRequired()
                .HasMaxLength(70);
            builder.Property(user => user.Dpto)
                .HasMaxLength(70);
            builder.Property(user => user.EntreCalles)
                .IsRequired()
                .HasMaxLength(70);

            builder.Property(user => user.Telefono)
                .IsRequired()
                .HasMaxLength(16);

            //builder.HasIndex(user => user.passwordHash)
            //    .IsUnique();

            builder.HasOne(u => u.Cliente)
               .WithOne(c => c.UsuarioRegistrado)
               .HasForeignKey<Cliente>(c => c.DNI);
        }
    }
}
