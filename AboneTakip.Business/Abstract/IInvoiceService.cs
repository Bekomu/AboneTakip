using AboneTakip.API.Core.Result;
using AboneTakip.DTOs.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.Business.Abstract
{
    public interface IInvoiceService
    {
        Task<IDataResult<List<InvoiceDTO>>> GetAll();
        Task<IDataResult<List<InvoiceDTO>>> AddByUsage(InvoiceAllReadingsCreateDTO invoiceCreateDTO);
        Task<IDataResult<InvoiceDTO>> AddByReading(InvoiceSpecificReadingCreateDTO invoiceSpecificReadingCreateDTO);
        Task<IDataResult<List<InvoiceDTO>>> AddByVolumetricBuy(InvoiceAllReadingsCreateDTO invoiceCreateDTO);
        Task<IDataResult<InvoiceDTO>> Update(InvoiceUpdateDTO invoiceUpdateDTO);
        Task<IResult> Delete(Guid id);
    }
}
