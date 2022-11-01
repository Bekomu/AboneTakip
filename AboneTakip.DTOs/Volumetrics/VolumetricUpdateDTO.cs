using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.DTOs.Volumetrics
{
    public class VolumetricUpdateDTO
    {
        public Guid Id { get; set; }
        public decimal LastIndex { get; set; }
        public decimal PreloadVolume { get; set; }
        public bool IsInvoiced { get; set; }
        public virtual Guid CustomerId { get; set; }
    }
}
