using AboneTakip.DTOs.Invoices;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.Business.EntityValidation.Concrete.Invoices
{
    public class InvoiceValidator : AbstractValidator<InvoiceAllReadingsCreateDTO>
    {
        static readonly int maxLength = 250;

        readonly string notNullMessage = "This value is required";

        public InvoiceValidator()
        {
            RuleFor(x => x.CustomerId).NotNull().WithMessage(notNullMessage)
                .NotEmpty().WithMessage(notNullMessage);
        }
    }
}
