using AboneTakip.DTOs.Customers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.Business.EntityValidation.Concrete.Customers
{
    public class UpdateCustomerValidator : AbstractValidator<CustomerUpdateDTO>
    {
        static readonly int maxLength = 250;

        readonly string notNullMessage = "This value is required";
        readonly string KDVRateMessage = "This value must be integer 1 = 1%, 8 = 8% or 18 = 18%";
        readonly string currencyMessage = "This value must be integer ! 1 = TRY, 2 = USD, 3 = EUR or 4 = GBP";

        public UpdateCustomerValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage(notNullMessage).NotEmpty().WithMessage(notNullMessage);

            RuleFor(x => x.Adress).NotNull().WithMessage(notNullMessage)
                .NotEmpty().WithMessage(notNullMessage)
                .MaximumLength(maxLength);

            RuleFor(x => x.Name).NotNull().WithMessage(notNullMessage)
                .NotEmpty().WithMessage(notNullMessage);

            RuleFor(x => x.Surname).NotNull().WithMessage(notNullMessage)
                .NotEmpty().WithMessage(notNullMessage);

            RuleFor(x => x.Currency).IsInEnum().WithMessage(currencyMessage)
                .NotEmpty().WithMessage(notNullMessage)
                .NotNull().WithMessage(notNullMessage);

            RuleFor(x => x.KDVRate).IsInEnum().WithMessage(KDVRateMessage)
                .NotEmpty().WithMessage(notNullMessage)
                .NotNull().WithMessage(notNullMessage);
        }
    }
}
