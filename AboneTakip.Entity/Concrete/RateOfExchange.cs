using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.Entity.Concrete
{
    public class RateOfExchange
    {
        public string Tarih { get; set; }
        public string TP_DK_USD_S { get; set; }
        public string TP_DK_EUR_S { get; set; }
        public string TP_DK_GBP_S { get; set; }
        public object UNIXTIME { get; set; }
    }
}
