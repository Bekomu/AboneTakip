using AboneTakip.Core.DataAccess;
using AboneTakip.DataAccess.Abstract;
using AboneTakip.DataAccess.EntitiyFramework.Context;
using AboneTakip.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.DataAccess.EntitiyFramework.Concrete
{
    public class CustomerRepository : BaseRepository<Customer, AboneTakipDbContext>, ICustomerRepository
    {
        public CustomerRepository(AboneTakipDbContext context) : base(context) { }
    }
}
