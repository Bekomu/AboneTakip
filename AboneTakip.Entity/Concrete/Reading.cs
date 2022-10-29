using AboneTakip.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.Entity.Concrete
{
    public class Reading : BaseEntity
    {
        public decimal FirstIndex { get; set; }
        public decimal LastIndex { get; set; }
    }
}
