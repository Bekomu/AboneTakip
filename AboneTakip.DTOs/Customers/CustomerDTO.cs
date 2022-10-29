using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.DTOs.Customers
{
    public class CustomerDTO
    {
        public Guid Id { get; set; }
        public DateTime SubscribedDate { get; set; }
        public string Adress { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Currency { get; set; }
        public int KDVRate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public Guid UsageInfoId { get; set; }
    }
}
