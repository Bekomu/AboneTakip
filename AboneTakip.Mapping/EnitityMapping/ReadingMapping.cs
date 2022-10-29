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
    public class ReadingMapping : MapBase<Reading>
    {
        public override void Configure(EntityTypeBuilder<Reading> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.FirstIndex).IsRequired(true);
            builder.Property(x => x.LastIndex).IsRequired(true);
        }
    }
}
