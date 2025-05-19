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
    public class RecargoConfig : IEntityTypeConfiguration<Recargo>
    {
        public void Configure(EntityTypeBuilder<Recargo> builder)
        {
            builder.HasKey(r => new { r.idRecargo, r.idDevolucion });

            builder.Property(r => r.Total)
                .HasColumnType("decimal(18,2)");

            builder.Property(r => r.Descripcion)
                .HasMaxLength(150);

            builder.Property(r => r.Status)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasOne(r => r.Devolucion)
                .WithMany(d => d.Recargos)
                .HasForeignKey(r => r.idDevolucion)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
