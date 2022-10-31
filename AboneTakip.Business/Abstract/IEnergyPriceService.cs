using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.Business.Abstract
{
    public interface IEnergyPriceService
    {
        Task<decimal> GetEnergyPriceToday();

        // method can take today's price...
    }
}
