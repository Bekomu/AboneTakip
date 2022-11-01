using AboneTakip.API.Core.Result;
using AboneTakip.DTOs.Volumetrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.Business.Abstract
{
    public interface IVolumetricService
    {
        Task<IDataResult<List<VolumetricDTO>>> GetAll();
        Task<IDataResult<VolumetricDTO>> GetById(Guid id);
        Task<IDataResult<List<VolumetricDTO>>> GetCustomerVolumetric(Guid customerId);
        Task<IDataResult<VolumetricDTO>> GetCustomerLastVolumetric(Guid customerId);
        Task<IDataResult<List<VolumetricDTO>>> GetCustomerNotInvoicedPreloads(Guid customerId);
        Task<IDataResult<VolumetricDTO>> Add(VolumetricCreateDTO volumetricCreateDTO);
        Task<IDataResult<VolumetricDTO>> Update(VolumetricUpdateDTO volumetricUpdateDTO);
        Task<IResult> Delete(Guid id);
    }
}
