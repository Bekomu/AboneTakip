using AboneTakip.Business.Abstract;
using AboneTakip.Core.Enums;
using AboneTakip.DTOs.Invoices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AboneTakip.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet("GetAllInvoices")]
        public async Task<IActionResult> GetAllInvoices()
        {
            var result = await _invoiceService.GetAll();

            if (result.ResultStatus != ResultStatus.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _invoiceService.GetById(id);

            if (result.ResultStatus != ResultStatus.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetInvoicesByCustomerId")]
        public async Task<IActionResult> GetInvoicesByCustomerId(Guid id)
        {
            var result = await _invoiceService.GetByCustomerId(id);

            if (result.ResultStatus != ResultStatus.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("CreateInvoiceByCustomerUsage")]
        public async Task<IActionResult> CreateInvoiceByCustomerUsage(InvoiceAllReadingsCreateDTO invoiceCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _invoiceService.AddByUsage(invoiceCreateDTO);

            if (result.ResultStatus != ResultStatus.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("CreateInvoiceBySpecificReading")]
        public async Task<IActionResult> CreateInvoiceBySpecificReading(InvoiceSpecificReadingCreateDTO invoiceSpecificReadingCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _invoiceService.AddByReading(invoiceSpecificReadingCreateDTO);

            if (result.ResultStatus != ResultStatus.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("UpdateInvoice")]
        public async Task<IActionResult> UpdateInvoice(InvoiceUpdateDTO invoiceUpdateDTO)
        {
            var result = await _invoiceService.Update(invoiceUpdateDTO);

            if (result.ResultStatus != ResultStatus.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("DeleteInvoice")]
        public async Task<IActionResult> DeleteInvoice(Guid id)
        {
            var result = await _invoiceService.Delete(id);

            if (result.ResultStatus != ResultStatus.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
