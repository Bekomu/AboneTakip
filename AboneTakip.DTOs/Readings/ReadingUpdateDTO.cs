using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.DTOs.Readings
{
    public class ReadingUpdateDTO
    {
        public Guid Id { get; set; }
        public virtual Guid CustomerId { get; set; }
        public decimal FirstIndex { get; set; }
        public decimal LastIndex { get; set; }
        public bool IsInvoiced { get; set; }
    }
}
