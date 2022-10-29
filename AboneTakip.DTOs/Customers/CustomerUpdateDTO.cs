﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.DTOs.Customers
{
    public class CustomerUpdateDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Currency { get; set; }
        public int KDVRate { get; set; }
    }
}