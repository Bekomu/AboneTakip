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
    public class CustomerMapping : MapBase<Customer>
    {
        public override void Configure(EntityTypeBuilder<Customer> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.SubscribedDate).IsRequired(true);
            builder.Property(x => x.Adress).IsRequired(true);
            builder.Property(x => x.Name).IsRequired(true);
            builder.Property(x => x.Surname).IsRequired(true);
            builder.Property(x => x.Currency).IsRequired(true);
            builder.Property(x => x.KDVRate).IsRequired(true);
        }
    }
}
