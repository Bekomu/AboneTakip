using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.DTOs.Readings
{
    public class ReadingCreateDTO
    {
        public decimal FirstIndex { get; set; }
        public decimal LastIndex { get; set; }
        public Guid CustomerId { get; set; }
    }
}
