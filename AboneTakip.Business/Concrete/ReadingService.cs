using AboneTakip.API.Core.Result;
using AboneTakip.Business.Abstract;
using AboneTakip.Core.Enums;
using AboneTakip.DataAccess.Abstract;
using AboneTakip.DataAccess.EntitiyFramework.Concrete;
using AboneTakip.DTOs.Readings;
using AboneTakip.Entity.Concrete;
using AutoMapper;

namespace AboneTakip.Business.Concrete
{
    public class ReadingService : IReadingService
    {
        private readonly IReadingRepository _readingRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public ReadingService(IReadingRepository readingRepository, ICustomerRepository customerRepository, IMapper mapper )
        {
            _readingRepository = readingRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<IDataResult<ReadingDTO>> Add(ReadingCreateDTO readingCreateDTO)
        {
            var customer = await _customerRepository.GetById(readingCreateDTO.CustomerId);

            if (customer == null)
            {
                return new DataResult<ReadingDTO>(ResultStatus.Error, "Customer not found.", null);
            }

            var reading = _mapper.Map<Reading>(readingCreateDTO);
            await _readingRepository.Add(reading);
            var readingDto = _mapper.Map<ReadingDTO>(reading);

            return new DataResult<ReadingDTO>(ResultStatus.Success, "Customer's reading successfully added.", readingDto);
        }

        public async Task<IResult> Delete(Guid id)
        {
            var reading = await _readingRepository.GetById(id);

            if (reading == null)
            {
                return new Result(ResultStatus.Error, "Reading not found.");
            }

            await _readingRepository.Delete(reading);

            return new Result(ResultStatus.Success, $"{reading.Id} reading succesfully deleted.");
        }

        public async Task<IDataResult<List<ReadingDTO>>> GetAll()
        {
            var readings = await _readingRepository.GetAll();

            if (!readings.Any())
            {
                return new DataResult<List<ReadingDTO>>(ResultStatus.Error, "There are no readings on database.", null);
            }

            var readingDtos = _mapper.Map<List<ReadingDTO>>(readings.OrderByDescending(x => x.CreatedDate));

            return new DataResult<List<ReadingDTO>>(ResultStatus.Success, readingDtos);
        }

        public async Task<IDataResult<ReadingDTO>> GetCustomerLastReading(Guid customerId)
        {
            var customer = await _customerRepository.GetById(customerId);

            if (customer == null)
            {
                return new DataResult<ReadingDTO>(ResultStatus.Error, "Customer not found.", null);
            }

            if (!customer.Readings.Any())
            {
                return new DataResult<ReadingDTO>(ResultStatus.Error, "There are no readings on this customer.", null);
            }

            var readingDto = _mapper.Map<ReadingDTO>(customer.Readings.OrderByDescending(x => x.CreatedDate).First());

            return new DataResult<ReadingDTO>(ResultStatus.Error, readingDto);
        }

        public async Task<IDataResult<List<ReadingDTO>>> GetCustomerReadings(Guid customerId)
        {
            var customer = await _customerRepository.GetById(customerId);

            if (customer == null)
            {
                return new DataResult<List<ReadingDTO>>(ResultStatus.Error, "Customer not found.", null);
            }

            if (!customer.Readings.Any()) 
            {
                return new DataResult<List<ReadingDTO>>(ResultStatus.Error, "There are no readings on this customer.", null);
            }

            var readingDtos = _mapper.Map<List<ReadingDTO>>(customer.Readings.OrderByDescending(x => x.CreatedDate));

            return new DataResult<List<ReadingDTO>>(ResultStatus.Success, readingDtos);
        }

        public async Task<IDataResult<List<ReadingDTO>>> GetCustomerNotInvoicedReadings(Guid customerId)
        {
            var customer = await _customerRepository.GetById(customerId);

            if (customer == null)
            {
                return new DataResult<List<ReadingDTO>>(ResultStatus.Error, "Customer not found.", null);
            }

            if (!customer.Readings.Any())
            {
                return new DataResult<List<ReadingDTO>>(ResultStatus.Error, "There are no readings on this customer.", null);
            }

            var readingDtos = _mapper.Map<List<ReadingDTO>>(customer.Readings.Where(x => x.IsInvoiced == false).ToList());

            return new DataResult<List<ReadingDTO>>(ResultStatus.Success, readingDtos);
        }

        public async Task<IDataResult<ReadingDTO>> Update(ReadingUpdateDTO readingUpdateDTO)
        {
            var reading = await _readingRepository.GetById(readingUpdateDTO.Id);
            var updateReading = _mapper.Map(readingUpdateDTO, reading);

            try
            {
                await _readingRepository.Update(updateReading);
            }
            catch (Exception)
            {
                return new DataResult<ReadingDTO>(ResultStatus.Error, "Error occured when updating reading properties. Are you missing some props ?", null);
            }

            var updatedReading = _mapper.Map<ReadingDTO>(updateReading);

            return new DataResult<ReadingDTO>(ResultStatus.Success, "Reading succesfully updated.", updatedReading);
        }
    }
}
