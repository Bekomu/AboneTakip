using AboneTakip.API.Core.Result;
using AboneTakip.Business.Abstract;
using AboneTakip.Core.Enums;
using AboneTakip.DataAccess.Abstract;
using AboneTakip.DTOs.Invoices;
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

        public InvoiceService(IInvoiceRepository invoiceRepository, IReadingRepository readingRepository, IVolumetricRepository volumetricRepository, ICustomerRepository customerRepository, IMapper mapper, IEnergyPriceService energyPriceService)
        {
            _invoiceRepository = invoiceRepository;
            _readingRepository = readingRepository;
            _volumetricRepository = volumetricRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
            _energyPriceService = energyPriceService;
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

            if (reading == null)
            {
                return new DataResult<InvoiceDTO>(ResultStatus.Error, "Reading not found.", null);
            }
            if (reading.IsInvoiced == true)
            {
                return new DataResult<InvoiceDTO>(ResultStatus.Error, "You are trying to invoice which is Reading invoiced.", null);
            }

            InvoiceDTO invoiceDto = await CreateInvoiceByReading(invoiceSpecificReadingCreateDTO, reading);

            var invoice = _mapper.Map<Invoice>(invoiceDto);
            await _invoiceRepository.Add(invoice);
            var invoiceDtoResult = _mapper.Map<InvoiceDTO>(invoice);

            return new DataResult<InvoiceDTO>(ResultStatus.Success, invoiceDto);
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

            foreach (var reading in notInvoicedReadings)
            {
                var totalUsage = reading.LastIndex - reading.FirstIndex;
                var currentCurrencyValue = 1m; // customer ın para biriminin bugünkü tl karşılığı gelecek.
                var currentEnergyPrice = await _energyPriceService.GetEnergyPriceToday() / currentCurrencyValue;
                var customerTaxRate = (int)customer.KDVRate;
                var totalInvoiceWithTaxes = totalUsage * currentCurrencyValue * currentEnergyPrice * (1 + ((decimal)customerTaxRate / 100));
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


        public Task<IDataResult<List<InvoiceDTO>>> AddByVolumetricBuy(InvoiceAllReadingsCreateDTO invoiceCreateDTO)
        {
            // TODO : volumetric satın alıma göre hesaplama yapılıp fatura oluşturulacak.



            return null;
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


        private async Task<InvoiceDTO> CreateInvoiceByReading(InvoiceSpecificReadingCreateDTO invoiceSpecificReadingCreateDTO, Reading reading)
        {
            var totalUsage = reading.LastIndex - reading.FirstIndex;
            var currentCurrencyValue = 1m; // customer ın para biriminin bugünkü tl karşılığı gelecek.
            var currentEnergyPrice = await _energyPriceService.GetEnergyPriceToday() / currentCurrencyValue;
            var customerTaxRate = (int)reading.Customer.KDVRate;
            var totalInvoiceWithTaxes = totalUsage * currentCurrencyValue * currentEnergyPrice * (1 + ((decimal)customerTaxRate / 100));
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
