using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.DTOs.Customers
{
    public class CustomerCreateDTO
    {
        public DateTime SubscribedDate { get; set; }
        public string Adress { get; set; }
        public decimal Phone { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Currency { get; set; }
        public int KDVRate { get; set; }
    }
}
