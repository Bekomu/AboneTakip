using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.Entity.Concrete
{
    public class EVDSResult
    {
        public int TotalCount { get; set; }
        public List<RateOfExchange> Items { get; set; }
    }
}
