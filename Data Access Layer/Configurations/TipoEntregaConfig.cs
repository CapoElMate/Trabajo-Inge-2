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
    public class TipoEntregaConfig : IEntityTypeConfiguration<TipoEntrega>
    {
        public void Configure(EntityTypeBuilder<TipoEntrega> builder)
        {
            builder.HasKey(tipo => tipo.Entrega);
        }
    }
}
