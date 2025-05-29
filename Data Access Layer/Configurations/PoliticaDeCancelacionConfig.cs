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
    public class PoliticaDeCancelacionConfig: IEntityTypeConfiguration<PoliticaDeCancelacion>
    {
        public void Configure(EntityTypeBuilder<PoliticaDeCancelacion> builder)
        {
            builder.HasKey(x => x.Politica);
            builder.Property(x => x.Descripcion).IsRequired();
        }
    }
}
