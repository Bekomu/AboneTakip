using AboneTakip.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.Entity.Concrete
{
    public class Reading : BaseEntity
    {
        public decimal FirstIndex { get; set; }
        public decimal LastIndex { get; set; }
        public bool IsInvoiced { get; set; }

        [ForeignKey("Customer")]
        public virtual Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
