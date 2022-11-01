using AboneTakip.API.Core.Result;
using AboneTakip.DTOs.Readings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.Business.Abstract
{
    public interface IReadingService
    {
        Task<IDataResult<List<ReadingDTO>>> GetAll();
        Task<IDataResult<ReadingDTO>> GetById(Guid id);
        Task<IDataResult<List<ReadingDTO>>> GetCustomerReadings(Guid customerId);
        Task<IDataResult<ReadingDTO>> GetCustomerLastReading(Guid customerId);
        Task<IDataResult<List<ReadingDTO>>> GetCustomerNotInvoicedReadings(Guid customerId);
        Task<IDataResult<ReadingDTO>> Add(ReadingCreateDTO readingCreateDTO);
        Task<IDataResult<ReadingDTO>> Update(ReadingUpdateDTO readingUpdateDTO);
        Task<IResult> Delete(Guid id);

    }
}
