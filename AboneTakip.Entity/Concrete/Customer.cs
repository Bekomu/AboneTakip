using AboneTakip.Core.Entities.Abstract;
using AboneTakip.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.Entity.Concrete
{
    public class Customer : BaseEntity
    {
        public DateTime SubscribedDate { get; set; }
        public string Adress { get; set; }
        public decimal Phone { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Currency Currency { get; set; }
        public KDVRate KDVRate { get; set; }

        public virtual List<Volumetric> VolumeUsage { get; set; } = new List<Volumetric> { };
        public virtual List<Reading> Readings { get; set; } = new List<Reading> { };
    }
}
