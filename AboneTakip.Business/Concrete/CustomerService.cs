using AboneTakip.API.Core.Result;
using AboneTakip.Business.Abstract;
using AboneTakip.Core.Enums;
using AboneTakip.DataAccess.Abstract;
using AboneTakip.DTOs.Customers;
using AboneTakip.Entity.Concrete;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.Business.Concrete
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }


        public async Task<IDataResult<CustomerDTO>> Add(CustomerCreateDTO customerCreateDTO)
        {
            var createCustomer = _mapper.Map<Customer>(customerCreateDTO);
            await _customerRepository.Add(createCustomer);
            var result = _mapper.Map<CustomerDTO>(createCustomer);

            return new DataResult<CustomerDTO>(ResultStatus.Success, result);
        }


        public async Task<IResult> Delete(Guid id)
        {
            var customer = await _customerRepository.GetById(id);
            if (customer == null)
            {
                return new Result(ResultStatus.Error, message: "Customer not found!");
            }
            await _customerRepository.Delete(customer);

            return new Result(ResultStatus.Success, message: $"Customer {customer.Name} {customer.Surname} ({customer.Id}) deleted.");
        }


        public async Task<IDataResult<List<CustomerDTO>>> GetAll()
        {
            var customers = await _customerRepository.GetAll();
            var customerDTOs = _mapper.Map<List<CustomerDTO>>(customers);

            return new DataResult<List<CustomerDTO>>(ResultStatus.Success, customerDTOs);
        }


        public async Task<IDataResult<CustomerDTO>> GetById(Guid id)
        {
            var customer = await _customerRepository.GetById(id);
            if (customer == null)
            {
                return new DataResult<CustomerDTO>(ResultStatus.Error, message: "Customer not found!", null);
            }
            var customerDTO = _mapper.Map<CustomerDTO>(customer);

            return new DataResult<CustomerDTO>(ResultStatus.Success, customerDTO);
        }


        public async Task<IDataResult<CustomerDTO>> Update(CustomerUpdateDTO customerUpdateDTO)
        {
            var customer = await _customerRepository.GetById(customerUpdateDTO.Id);
            var updateCustomer = _mapper.Map(customerUpdateDTO, customer);
            try
            {
                await _customerRepository.Update(updateCustomer);
            }
            catch (Exception)
            {
                return new DataResult<CustomerDTO>(ResultStatus.Error, "Error occured when updating customer properties. Are you missing UsageInfo or etc. ? If it's null, you should remove UsageInfoId line from request body.", null);
            }

            var updatedCustomerDto = _mapper.Map<CustomerDTO>(updateCustomer);

            return new DataResult<CustomerDTO>(ResultStatus.Success, "Customer properties succesfully updated.", updatedCustomerDto);
        }
    }
}
