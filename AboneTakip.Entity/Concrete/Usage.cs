using AboneTakip.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.Entity.Concrete
{
    public class Usage : BaseEntity
    {
        public virtual List<Volumetric> VolumeUsage { get; set; }
        
        public virtual List<Reading> Readings { get; set; }
    }
}
