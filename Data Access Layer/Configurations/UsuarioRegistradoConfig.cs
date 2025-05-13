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
            builder.Property(user => user.DNI)
                .IsRequired();
            builder.HasKey(user => user.DNI);
        }
    }
}
