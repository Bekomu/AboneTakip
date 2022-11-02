using AboneTakip.API.Core.Result;
using AboneTakip.Business.Abstract;
using AboneTakip.Core.Enums;
using AboneTakip.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.Business.Concrete
{
    public class RateOfExchangeService : IRateOfExchangeService
    {
        private const string URL = "https://evds2.tcmb.gov.tr/service/evds/series=TP.DK.USD.S-TP.DK.EUR.S-TP.DK.GBP.S&startDate=31-10-2022&endDate=31-10-2022&type=json&key=XXXXXXXX";

        string USDTL, EURTL, GBPTL;

        HttpResponseMessage response;
        HttpClient client;

        public decimal GetRateOfExchange(int customerCurrencyId)
        {
            try
            {
                client = new HttpClient();
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = client.GetAsync(client.BaseAddress).Result;

            }
            catch (Exception)
            {
                return 0m;
            }

            if (response.IsSuccessStatusCode)
            {
                var responseData = response.Content.ReadAsAsync<EVDSResult>().Result;
                foreach (var item in responseData.Items)
                {
                    USDTL = item.TP_DK_USD_S.ToString();
                    EURTL = item.TP_DK_EUR_S.ToString();
                    GBPTL = item.TP_DK_GBP_S.ToString();
                }
            }
            else
            {
                return 0m;
            }

            switch (customerCurrencyId)
            {
                case 1:
                    return 1m;
                case 2:
                    return Convert.ToDecimal(USDTL);
                case 3:
                    return Convert.ToDecimal(EURTL);
                case 4:
                    return Convert.ToDecimal(GBPTL);
                default:
                    return 0m;
            }
        }
    }
}
