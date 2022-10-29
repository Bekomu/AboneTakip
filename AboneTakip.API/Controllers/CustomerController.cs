using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AboneTakip.API.Controllers
{
    [Route("api/Customer/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // TODO : tümünü çekecesin.

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            // TODO : bir tane abone çekeceksin.

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Add()
        {
            // TODO: frombody den dto çekeceksin. burada ekleyeceksin.

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update()
        {
            // TODO: frombody den dto çekeceksin. burada update edeceksin.

            return Ok();
        }
    }
}
