using AboneTakip.DTOs.Readings;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.Business.EntityValidation.Concrete.Readings
{
    public class ReadingValidator : AbstractValidator<ReadingDTO>
    {
        readonly string notNullMessage = "This value is required";
        readonly string lessThenZeroMessage = "This value must be greater then 0";

        public ReadingValidator()
        {
            RuleFor(x => x.LastIndex).GreaterThan(0m).WithMessage(lessThenZeroMessage)
                .NotNull().WithMessage(notNullMessage)
                .NotEmpty().WithMessage(notNullMessage);
        }
    }
}
