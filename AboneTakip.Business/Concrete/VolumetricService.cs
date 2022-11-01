using AboneTakip.API.Core.Result;
using AboneTakip.Business.Abstract;
using AboneTakip.Core.Enums;
using AboneTakip.DataAccess.Abstract;
using AboneTakip.DTOs.Volumetrics;
using AboneTakip.Entity.Concrete;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.Business.Concrete
{
    public class VolumetricService : IVolumetricService
    {
        private readonly IVolumetricRepository _volumetricRepository;
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        public VolumetricService(IVolumetricRepository volumetricRepository, IMapper mapper, ICustomerRepository customerRepository)
        {
            _volumetricRepository = volumetricRepository;
            _mapper = mapper;
            _customerRepository = customerRepository;
        }

        public async Task<IDataResult<VolumetricDTO>> Add(VolumetricCreateDTO volumetricCreateDTO)
        {
            var createVolumetric = _mapper.Map<Volumetric>(volumetricCreateDTO);
            await _volumetricRepository.Add(createVolumetric);
            var result = _mapper.Map<VolumetricDTO>(createVolumetric);

            return new DataResult<VolumetricDTO>(ResultStatus.Success, result);
        }

        public async Task<IResult> Delete(Guid id)
        {
            var volumetric = await _volumetricRepository.GetById(id);
            if (volumetric == null)
            {
                return new Result(ResultStatus.Error, "Volumetric not found.");
            }
            var result = await _volumetricRepository.Delete(volumetric);

            return new Result(ResultStatus.Success, $"Volumetric {volumetric.Id} deleted succesfully.");
        }

        public async Task<IDataResult<List<VolumetricDTO>>> GetAll()
        {
            var volumetrics = await _volumetricRepository.GetAll();
            if (volumetrics == null)
            {
                return new DataResult<List<VolumetricDTO>>(ResultStatus.Error, "Volumetrics not found.", null);
            }
            var volumetricDtos = _mapper.Map<List<VolumetricDTO>>(volumetrics);

            return new DataResult<List<VolumetricDTO>>(ResultStatus.Success, volumetricDtos);
        }

        public async Task<IDataResult<VolumetricDTO>> GetById(Guid id)
        {
            var volumetric = await _volumetricRepository.GetById(id);
            if (volumetric == null)
            {
                return new DataResult<VolumetricDTO>(ResultStatus.Error, "Volumetric not found.", null);
            }
            var volumetricDto = _mapper.Map<VolumetricDTO>(volumetric);

            return new DataResult<VolumetricDTO>(ResultStatus.Success, volumetricDto);
        }

        public async Task<IDataResult<VolumetricDTO>> GetCustomerLastVolumetric(Guid customerId)
        {
            var customer = await _customerRepository.GetById(customerId);

            if (customer == null)
            {
                return new DataResult<VolumetricDTO>(ResultStatus.Error, "Customer not found.", null);
            }

            if (!customer.VolumeUsage.Any())
            {
                return new DataResult<VolumetricDTO>(ResultStatus.Error, "There are no volumetric on this customer.", null);
            }

            var volumetricDto = _mapper.Map<VolumetricDTO>(customer.VolumeUsage.OrderByDescending(x => x.CreatedDate).First());

            return new DataResult<VolumetricDTO>(ResultStatus.Success, volumetricDto);
        }

        public async Task<IDataResult<List<VolumetricDTO>>> GetCustomerNotInvoicedPreloads(Guid customerId)
        {
            var customer = await _customerRepository.GetById(customerId);

            if (customer == null)
            {
                return new DataResult<List<VolumetricDTO>>(ResultStatus.Error, "Customer not found.", null);
            }

            if (!customer.VolumeUsage.Any())
            {
                return new DataResult<List<VolumetricDTO>>(ResultStatus.Error, "There are no volumetrics on this customer.", null);
            }

            var volumetricDtos = _mapper.Map<List<VolumetricDTO>>(customer.VolumeUsage.Where(x => x.IsInvoiced == false).OrderByDescending(x => x.CreatedDate).ToList());

            return new DataResult<List<VolumetricDTO>>(ResultStatus.Success, volumetricDtos);
        }

        public async Task<IDataResult<List<VolumetricDTO>>> GetCustomerVolumetric(Guid customerId)
        {
            var customer = await _customerRepository.GetById(customerId);

            if (customer == null)
            {
                return new DataResult<List<VolumetricDTO>>(ResultStatus.Error, "Customer not found.", null);
            }

            if (!customer.VolumeUsage.Any())
            {
                return new DataResult<List<VolumetricDTO>>(ResultStatus.Error, "There are no volumetrics on this customer.", null);
            }

            var volumetricDtos = _mapper.Map<List<VolumetricDTO>>(customer.VolumeUsage.OrderByDescending(x => x.CreatedDate));

            return new DataResult<List<VolumetricDTO>>(ResultStatus.Success, volumetricDtos);
        }

        public async Task<IDataResult<VolumetricDTO>> Update(VolumetricUpdateDTO volumetricUpdateDTO)
        {
            var volumetric = await _volumetricRepository.GetById(volumetricUpdateDTO.Id);
            var updateVolumetric = _mapper.Map(volumetricUpdateDTO, volumetric);

            try
            {
                await _volumetricRepository.Update(updateVolumetric);
            }
            catch (Exception)
            {
                return new DataResult<VolumetricDTO>(ResultStatus.Error, "Error occured when updating reading properties. Are you missing some props ?", null);
            }

            var updatedVolumetric = _mapper.Map<VolumetricDTO>(updateVolumetric);
            
            return new DataResult<VolumetricDTO>(ResultStatus.Success, "Volumetric succesfully updated.", updatedVolumetric);
        }
    }
}
