using AboneTakip.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.Entity.Concrete
{
    public class Volumetric : BaseEntity
    {
        public decimal LastIndex { get; set; }
        public decimal PreloadVolume { get; set; }  
    }
}
