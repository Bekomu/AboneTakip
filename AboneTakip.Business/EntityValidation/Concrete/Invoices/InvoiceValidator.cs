using AboneTakip.DTOs.Invoices;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.Business.EntityValidation.Concrete.Invoices
{
    public class InvoiceValidator : AbstractValidator<InvoiceDTO>
    {
        static readonly int maxLength = 250;

        readonly string notNullMessage = "This value is required";
        readonly string lessThenZeroMessage = "This value must be greater then 0";
        readonly string lessTodayDate = "First Reading must be an earlier date.";
        readonly string lessLastDate = "Last Reading can not be future date.";

        public InvoiceValidator()
        {
            RuleFor(x => x.InvoiceAmount).GreaterThan(0m).WithMessage(lessThenZeroMessage)
                .NotNull().WithMessage(notNullMessage)
                .NotEmpty().WithMessage(notNullMessage);

            RuleFor(x => x.TotalUsage).GreaterThan(0m).WithMessage(lessThenZeroMessage);

            RuleFor(x => x.VolumetricPreload).GreaterThan(0m).WithMessage(lessThenZeroMessage);

            RuleFor(x => x.FirstReading).LessThan(DateTime.Now).WithMessage(lessTodayDate);

            RuleFor(x => x.LastReading).LessThan(DateTime.Now).WithMessage(lessLastDate);

            RuleFor(x => x.CustomerId).NotNull().WithMessage(notNullMessage)
                .NotEmpty().WithMessage(notNullMessage);
        }
    }
}
