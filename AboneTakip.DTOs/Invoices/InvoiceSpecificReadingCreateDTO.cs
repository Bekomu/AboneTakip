using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.DTOs.Invoices
{
    public class InvoiceSpecificReadingCreateDTO
    {
        public DateTime FirstReading { get; set; }
        public DateTime LastReading { get; set; }
        public Guid ReadingId { get; set; }
    }
}
