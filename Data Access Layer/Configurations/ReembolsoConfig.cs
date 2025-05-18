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
    public class ReembolsoConfig : IEntityTypeConfiguration<Reembolso>
    {
        public void Configure(EntityTypeBuilder<Reembolso> builder)
        {
            builder.HasKey(r => new { r.idReembolso, r.DNICliente });
            builder.Property(r => r.Motivo)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(r => r.Status)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(r => r.Monto)
                .IsRequired()
                .HasPrecision(18, 2);
        }
    }
}
