using AboneTakip.API.Core.Result;
using AboneTakip.DTOs.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.Business.Abstract
{
    public interface ICustomerService
    {
        Task<IDataResult<List<CustomerDTO>>> GetAll();
        Task<IDataResult<CustomerDTO>> GetById(Guid id);
        Task<IDataResult<CustomerDTO>> Add(CustomerCreateDTO customerCreateDTO); 
        Task<IDataResult<CustomerDTO>> Update(CustomerUpdateDTO customerUpdateDTO);
        Task<IResult> Delete(Guid id);
    }
}
