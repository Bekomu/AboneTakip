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
    public class InvoiceMapping : MapBase<Invoice>
    {
        public override void Configure(EntityTypeBuilder<Invoice> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.InvoiceAmount).IsRequired(true);
        }
    }
}
