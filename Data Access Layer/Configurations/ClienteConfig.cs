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
    public class ClienteConfig : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            // Primary Key
            builder.HasKey(cliente => cliente.DNI);

            builder.Property(c => c.DNI)
                  .ValueGeneratedNever();

            builder.Property(cliente => cliente.DNI)
                .IsRequired();

            builder.HasIndex(cliente => cliente.DNI)
                .IsUnique();

            // Relación uno a uno con UsuarioRegistrado
            builder.HasOne(c => c.UsuarioRegistrado)
                .WithOne(u => u.Cliente)
                .HasForeignKey<Cliente>(c => c.DNI)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
