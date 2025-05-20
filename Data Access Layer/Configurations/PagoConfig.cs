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
    public class PagoConfig : IEntityTypeConfiguration<Pago>
    {
        public void Configure(EntityTypeBuilder<Pago> builder)
        {
            builder.HasKey(p => p.nroPago);

            builder.Property(p => p.fecPago)
                .IsRequired();

            builder.Property(p => p.idReserva)
                .IsRequired();

            builder.HasOne(p => p.Reserva)
                .WithOne(r => r.Pago)
                .HasForeignKey<Pago>(p => p.idReserva)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
