using AboneTakip.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.Business.Concrete
{
    public class EnergyPriceService : IEnergyPriceService
    {
        public async Task<decimal> GetEnergyPriceToday()
        {
            return 5.2835m;
        }
    }
}
