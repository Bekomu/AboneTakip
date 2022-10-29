using AboneTakip.Core.Entities.Abstract;
using AboneTakip.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.Entity.Concrete
{
    public class Abone : BaseEntity
    {
        public DateTime AccountCreateDate { get; set; }
        public string Adress { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Currency Currency { get; set; }
        public KDVRate KDVRate { get; set; }
    }
}
