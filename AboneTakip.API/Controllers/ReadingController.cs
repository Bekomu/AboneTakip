using AboneTakip.Business.Abstract;
using AboneTakip.Core.Enums;
using AboneTakip.DTOs.Readings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AboneTakip.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadingController : ControllerBase
    {
        private readonly IReadingService _readingService;

        public ReadingController(IReadingService readingService)
        {
            _readingService = readingService;
        }

        [HttpGet("GetAllReadings")]
        public async Task<IActionResult> GetAllReadings()
        {
            var result = await _readingService.GetAll();

            if (result.ResultStatus != ResultStatus.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetReadingById")]
        public async Task<IActionResult> GetReadingById(Guid id)
        {
            var result = await _readingService.GetById(id);

            if (result.ResultStatus != ResultStatus.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetCustomerReadings")]
        public async Task<IActionResult> GetCustomerReadings(Guid id)
        {
            var result = await _readingService.GetCustomerReadings(id);

            if (result.ResultStatus != ResultStatus.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetCustomerLastReading")]
        public async Task<IActionResult> GetCustomerLastReading(Guid id)
        {
            var result = await _readingService.GetCustomerLastReading(id);

            if (result.ResultStatus != ResultStatus.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetCustomerNotInvoicedReadings")]
        public async Task<IActionResult> GetCustomerNotInvoicedReadings(Guid id)
        {
            var result = await _readingService.GetCustomerNotInvoicedReadings(id);

            if (result.ResultStatus != ResultStatus.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("AddReading")]
        public async Task<IActionResult> AddReading(ReadingCreateDTO readingCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _readingService.Add(readingCreateDTO);

            if (result.ResultStatus != ResultStatus.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("UpdateReading")]
        public async Task<IActionResult> UpdateReading(ReadingUpdateDTO readingUpdateDTO)
        {
            var result = await _readingService.Update(readingUpdateDTO);

            if (result.ResultStatus != ResultStatus.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("DeleteReading")]
        public async Task<IActionResult> DeleteReading(Guid id)
        {
            var result = await _readingService.Delete(id);

            if (result.ResultStatus != ResultStatus.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
