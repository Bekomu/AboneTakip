using AboneTakip.API.Core.Result;
using AboneTakip.Business.Abstract;
using AboneTakip.Core.Enums;
using AboneTakip.DataAccess.Abstract;
using AboneTakip.DTOs.Invoices;
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
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IReadingRepository _readingRepository;
        private readonly IVolumetricRepository _volumetricRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IEnergyPriceService _energyPriceService;
        private readonly IRateOfExchangeService _rateOfExchangeService;

        public InvoiceService(IInvoiceRepository invoiceRepository, IReadingRepository readingRepository, IVolumetricRepository volumetricRepository, ICustomerRepository customerRepository, IMapper mapper, IEnergyPriceService energyPriceService, IRateOfExchangeService rateOfExchangeService)
        {
            _invoiceRepository = invoiceRepository;
            _readingRepository = readingRepository;
            _volumetricRepository = volumetricRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
            _energyPriceService = energyPriceService;
            _rateOfExchangeService = rateOfExchangeService;
        }


        public async Task<IDataResult<List<InvoiceDTO>>> GetAll()
        {
            var invoices = (await _invoiceRepository.GetAll()).OrderByDescending(x => x.CreatedBy).ToList();
            var invoiceDtos = _mapper.Map<List<InvoiceDTO>>(invoices);

            return new DataResult<List<InvoiceDTO>>(ResultStatus.Success, invoiceDtos);
        }


        public async Task<IDataResult<InvoiceDTO>> GetById(Guid id)
        {
            var invoice = (await _invoiceRepository.GetAll()).FirstOrDefault(x => x.Id == id);
            var invoiceDto = _mapper.Map<InvoiceDTO>(invoice);

            return new DataResult<InvoiceDTO>(ResultStatus.Success, invoiceDto);
        }


        public async Task<IDataResult<List<InvoiceDTO>>> GetByCustomerId(Guid id)
        {
            var invoices = (await _invoiceRepository.GetAll()).Where(x => x.CustomerId == id).OrderByDescending(x => x.CreatedBy).ToList();
            var invoiceDtos = _mapper.Map<List<InvoiceDTO>>(invoices);

            return new DataResult<List<InvoiceDTO>>(ResultStatus.Success, invoiceDtos);
        }


        public async Task<IDataResult<InvoiceDTO>> AddByReading(InvoiceSpecificReadingCreateDTO invoiceSpecificReadingCreateDTO)
        {
            var reading = await _readingRepository.GetById(invoiceSpecificReadingCreateDTO.ReadingId);
            var customer = reading.Customer;

            if (reading == null)
            {
                return new DataResult<InvoiceDTO>(ResultStatus.Error, "Reading not found.", null);
            }
            if (reading.IsInvoiced == true)
            {
                return new DataResult<InvoiceDTO>(ResultStatus.Error, "You are trying to invoice which is Reading invoiced.", null);
            }

            var currentRateOfExchange = _rateOfExchangeService.GetRateOfExchange((int)customer.Currency);
            if (currentRateOfExchange == 0m)
            {
                return new DataResult<InvoiceDTO>(ResultStatus.Error, "Error occured when calling EVDS Rate of Exchange system. Please try again later", null);
            }

            InvoiceDTO invoiceDto = await CreateInvoiceByReading(invoiceSpecificReadingCreateDTO, reading, currentRateOfExchange);

            var invoice = _mapper.Map<Invoice>(invoiceDto);
            await _invoiceRepository.Add(invoice);
            var invoiceDtoResult = _mapper.Map<InvoiceDTO>(invoice);

            return new DataResult<InvoiceDTO>(ResultStatus.Success, invoiceDto);
        }


        public async Task<IDataResult<InvoiceDTO>> AddByVolumetricBuy(InvoiceVolumetricBuyCreateDTO invoiceVolumetricBuyCreateDTO)
        {
            var volumetric = await _volumetricRepository.GetById(invoiceVolumetricBuyCreateDTO.VolumetricId);
            var customer = volumetric.Customer;

            if (volumetric == null)
            {
                return new DataResult<InvoiceDTO>(ResultStatus.Error, "Volumetric not found.", null);
            }
            if (volumetric.IsInvoiced == true)
            {
                return new DataResult<InvoiceDTO>(ResultStatus.Error, "You are trying to invoice which is Preload invoiced.", null);
            }

            var currentRateOfExchange = _rateOfExchangeService.GetRateOfExchange((int)customer.Currency);
            if (currentRateOfExchange == 0m)
            {
                return new DataResult<InvoiceDTO>(ResultStatus.Error, "Error occured when calling EVDS Rate of Exchange system. Please try again later", null);
            }

            InvoiceDTO invoiceDto = await CreateInvoiceByVolumetricPreload(volumetric, currentRateOfExchange);

            var invoice = _mapper.Map<Invoice>(invoiceDto);
            await _invoiceRepository.Add(invoice);
            var invoiceDtoResult = _mapper.Map<InvoiceDTO>(invoice);

            return new DataResult<InvoiceDTO>(ResultStatus.Success, invoiceDtoResult);
        }


        public async Task<IDataResult<List<InvoiceDTO>>> AddByUsage(InvoiceAllReadingsCreateDTO invoiceCreateDTO)
        {
            var customer = await _customerRepository.GetById(invoiceCreateDTO.CustomerId);

            if (customer == null)
            {
                return new DataResult<List<InvoiceDTO>>(ResultStatus.Error, "Costumer not found.", null);
            }
            if (!customer.Readings.Any())
            {
                return new DataResult<List<InvoiceDTO>>(ResultStatus.Error, "There are no readings on this customer.", null);
            }
            if (customer.Readings.Any() && !customer.Readings.Where(x => x.IsInvoiced == false).Any())
            {
                return new DataResult<List<InvoiceDTO>>(ResultStatus.Error, "Costumer's all readings invoiced.", null);
            }

            var notInvoicedReadings = customer.Readings.Where(x => x.IsInvoiced == false).ToList();

            var toBeInvoicedReadings = new List<Invoice>();
            var invoicedReadingsDto = new List<InvoiceDTO>();
            var currentRateOfExchange = _rateOfExchangeService.GetRateOfExchange((int)customer.Currency);
            if (currentRateOfExchange == 0m)
            {
                return new DataResult<List<InvoiceDTO>>(ResultStatus.Error, "Error occured when calling EVDS Rate of Exchange system. Please try again later", null);
            }

            foreach (var reading in notInvoicedReadings)
            {
                var totalUsage = reading.LastIndex - reading.FirstIndex;
                var currentEnergyPrice = await _energyPriceService.GetEnergyPriceToday() / currentRateOfExchange;
                var customerTaxRate = (int)customer.KDVRate;
                var totalInvoiceWithTaxes = totalUsage * currentEnergyPrice * (1 + ((decimal)customerTaxRate / 100));
                reading.IsInvoiced = true;
                await _readingRepository.Update(reading);

                var invoiceDto = new InvoiceDTO()
                {
                    CustomerId = invoiceCreateDTO.CustomerId,
                    TotalUsage = totalUsage,
                    FirstReading = invoiceCreateDTO.FirstReading,
                    LastReading = invoiceCreateDTO.LastReading,
                    InvoiceAmount = totalInvoiceWithTaxes
                };

                var invoice = _mapper.Map<Invoice>(invoiceDto);
                await _invoiceRepository.Add(invoice);

                toBeInvoicedReadings.Add(invoice);
                invoicedReadingsDto = _mapper.Map<List<InvoiceDTO>>(toBeInvoicedReadings);
            }

            return new DataResult<List<InvoiceDTO>>(ResultStatus.Success, invoicedReadingsDto);
        }


        public async Task<IDataResult<InvoiceDTO>> Update(InvoiceUpdateDTO invoiceUpdateDTO)
        {
            var invoice = await _invoiceRepository.GetById(invoiceUpdateDTO.Id);
            var updateInvoice = _mapper.Map(invoiceUpdateDTO, invoice);

            try
            {
                await _invoiceRepository.Update(updateInvoice);
            }
            catch (Exception)
            {
                return new DataResult<InvoiceDTO>(ResultStatus.Error, "Error occured when updating invoice properties. Are you missing some props ?", null);
            }

            var updatedInvoice = _mapper.Map<InvoiceDTO>(updateInvoice);

            return new DataResult<InvoiceDTO>(ResultStatus.Success, "Invoice succesfully updated.", updatedInvoice);
        }


        public async Task<IResult> Delete(Guid id)
        {
            var invoice = await _invoiceRepository.GetById(id);

            if (invoice == null)
            {
                return new Result(ResultStatus.Error, "Invoice not found");
            }

            await _invoiceRepository.Delete(invoice);

            return new Result(ResultStatus.Success, $"Invoice ({invoice.Id}) deleted successfully.");
        }


        private async Task<InvoiceDTO> CreateInvoiceByVolumetricPreload(Volumetric volumetric, decimal currentRateOfExchange)
        {
            var volumetricPreload = volumetric.PreloadVolume;
            var currentEnergyPrice = await _energyPriceService.GetEnergyPriceToday() / currentRateOfExchange;
            var customerTaxRate = (int)volumetric.Customer.KDVRate;
            var totalInvoiceWithTaxes = volumetricPreload * currentEnergyPrice * (1 + ((decimal)customerTaxRate / 100));
            volumetric.IsInvoiced = true;
            await _volumetricRepository.Update(volumetric);

            var invoiceDto = new InvoiceDTO()
            {
                CustomerId = volumetric.Customer.Id,
                VolumetricPreload = volumetricPreload,
                FirstReading = DateTime.Now,
                LastReading = DateTime.Now,
                InvoiceAmount = totalInvoiceWithTaxes,
            };
            return invoiceDto;
        }


        private async Task<InvoiceDTO> CreateInvoiceByReading(InvoiceSpecificReadingCreateDTO invoiceSpecificReadingCreateDTO, Reading reading, decimal currentRateOfExchange)
        {
            var totalUsage = reading.LastIndex - reading.FirstIndex;
            var currentEnergyPrice = await _energyPriceService.GetEnergyPriceToday() / currentRateOfExchange;
            var customerTaxRate = (int)reading.Customer.KDVRate;
            var totalInvoiceWithTaxes = totalUsage * currentEnergyPrice * (1 + ((decimal)customerTaxRate / 100));
            reading.IsInvoiced = true;
            await _readingRepository.Update(reading);

            var invoiceDto = new InvoiceDTO()
            {
                CustomerId = reading.Customer.Id,
                TotalUsage = totalUsage,
                FirstReading = invoiceSpecificReadingCreateDTO.FirstReading,
                LastReading = invoiceSpecificReadingCreateDTO.LastReading,
                InvoiceAmount = totalInvoiceWithTaxes
            };
            return invoiceDto;
        }
    }
}
