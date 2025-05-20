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
    public class TagPublicacionConfig: IEntityTypeConfiguration<TagPublicacion>
    {
        public void Configure(EntityTypeBuilder<TagPublicacion> builder)
        {
            builder.HasKey(tag => tag.Tag);
        }
    }
}
