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

        [HttpPost("CreateInvoiceByUsage")]
        public async Task<IActionResult> CreateInvoiceByUsage(InvoiceAllReadingsCreateDTO invoiceCreateDTO)
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
    }
}
