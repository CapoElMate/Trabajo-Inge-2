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
            builder.Property(reserva => reserva.Piso)
                .HasMaxLength(70);

            // Relación con TipoEntrega (muchas Reservas a uno)
            builder.HasOne(reserva => reserva.TipoEntrega)
                .WithMany(te => te.Reservas)
                .HasForeignKey(reserva => reserva.Entrega)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación con Pago (uno a uno)
            builder.HasOne(reserva => reserva.Pago)
                .WithOne(p => p.Reserva)
                .HasForeignKey<Reserva>(reserva => reserva.nroPago)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación con Alquiler (uno a uno, puede ser nulo)
            builder.HasOne(reserva => reserva.Alquiler)
                .WithOne(a => a.Reserva)
                .HasForeignKey<Reserva>(reserva => reserva.idAlquiler)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación con Cliente (muchas Reservas a uno)
            builder.HasOne(reserva => reserva.Cliente)
                .WithMany(c => c.Reservas)
                .HasForeignKey(reserva => reserva.DNI)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación con Publicacion (muchas Reservas a una Publicacion)
            builder.HasOne(reserva => reserva.Publicacion)
                .WithMany(p => p.Reservas)
                .HasForeignKey(reserva => reserva.idPublicacion)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
