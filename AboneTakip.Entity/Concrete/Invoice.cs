using AboneTakip.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.Entity.Concrete
{
    public class Invoice : BaseEntity
    {
        public decimal InvoiceAmount { get; set; }
        public decimal TotalUsage { get; set; }
        public decimal VolumetricPreload { get; set; }
        public DateTime FirstReading { get; set; }
        public DateTime LastReading { get; set; }   
        public decimal DailyAverageUsage { get; set; }

        [ForeignKey("Customer")]
        public virtual Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
