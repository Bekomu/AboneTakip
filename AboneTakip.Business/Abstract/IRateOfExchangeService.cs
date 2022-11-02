using AboneTakip.API.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.Business.Abstract
{
    public interface IRateOfExchangeService
    {
        decimal GetRateOfExchange(int customerCurrencyId);
    }
}
