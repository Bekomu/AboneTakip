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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // TODO : tümünü çekecesin.
            var result = await _customerService.GetAll();

            if(result.ResultStatus != ResultStatus.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            // TODO : bir tane abone çekeceksin.
            var result = await _customerService.GetById(id);

            if (result.ResultStatus != ResultStatus.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CustomerCreateDTO customerCreateDTO)
        {
            // TODO: frombody den dto çekeceksin. burada ekleyeceksin.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _customerService.Add(customerCreateDTO);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update()
        {
            // TODO: frombody den dto çekeceksin. burada update edeceksin.

            return Ok();
        }
    }
}
