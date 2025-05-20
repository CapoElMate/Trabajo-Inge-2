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
    public class TagMaquinaConfig: IEntityTypeConfiguration<TagMaquina>
    {
        public void Configure(EntityTypeBuilder<TagMaquina> builder)
        {
            builder.HasKey(x => x.Tag);
        }
    }
}
