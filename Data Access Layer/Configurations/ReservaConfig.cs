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
    public class ReservaConfig: IEntityTypeConfiguration<Reserva>
    {
        public void Configure(EntityTypeBuilder<Reserva> builder)
        {
            builder.HasKey(reserva => reserva.idReserva);
            builder.Property(reserva => reserva.idReserva)
                .IsRequired();
            builder.HasIndex(reserva => reserva.idReserva)
                .IsUnique();
            builder.Property(reserva => reserva.fecInicio)
                .IsRequired();
            builder.Property(reserva => reserva.fecFin)
                .IsRequired();
            builder.Property(reserva => reserva.Status)
                .HasMaxLength(50);
            builder.Property(reserva => reserva.montoTotal)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            builder.Property(reserva => reserva.Calle)
                .HasMaxLength(70);
            builder.Property(reserva => reserva.Altura)
                .HasMaxLength(70);
            builder.Property(reserva => reserva.Dpto)
                .HasMaxLength(70);
            builder.Property(reserva => reserva.EntreCalles)
                .HasMaxLength(70);
        }
    }
}
