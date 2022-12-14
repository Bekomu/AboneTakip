using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.DTOs.Invoices
{
    public class InvoiceUpdateDTO
    {
        public Guid Id { get; set; }
        public decimal InvoiceAmount { get; set; }
        public decimal TotalUsage { get; set; }
        public decimal VolumetricPreload { get; set; }
        public DateTime FirstReading { get; set; }
        public DateTime LastReading { get; set; }
        public Guid CustomerId { get; set; }
    }
}
