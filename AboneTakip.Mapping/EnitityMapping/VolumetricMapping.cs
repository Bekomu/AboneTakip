using AboneTakip.Core.DataAccess;
using AboneTakip.Entity.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.Mapping.EnitityMapping
{
    public class VolumetricMapping : MapBase<Volumetric>
    {
        public override void Configure(EntityTypeBuilder<Volumetric> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.PreloadVolume).IsRequired(true);
        }
    }
}
