using AboneTakip.Business.Abstract;
using AboneTakip.Core.Enums;
using AboneTakip.DTOs.Customers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AboneTakip.API.Controllers
{
    [Route("api/Customer/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var result = await _customerService.GetAll();

            if(result.ResultStatus != ResultStatus.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetCustomerById")]
        public async Task<IActionResult> GetCustomerById(Guid id)
        {
            var result = await _customerService.GetById(id);

            if (result.ResultStatus != ResultStatus.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("AddCustomer")]
        public async Task<IActionResult> AddCustomer(CustomerCreateDTO customerCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _customerService.Add(customerCreateDTO);

            return Ok(result);
        }

        [HttpPut("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer(CustomerUpdateDTO customerUpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _customerService.Update(customerUpdateDTO);

            return Ok(result);
        }

        [HttpDelete("DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var result = await _customerService.Delete(id);

            if (result.ResultStatus != ResultStatus.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
