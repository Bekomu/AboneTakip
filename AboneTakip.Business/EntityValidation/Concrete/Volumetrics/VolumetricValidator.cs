using AboneTakip.DTOs.Volumetrics;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.Business.EntityValidation.Concrete.Volumetrics
{
    public class VolumetricValidator : AbstractValidator<VolumetricDTO>
    {
        readonly string notNullMessage = "This value is required";
        readonly string lessThenZeroMessage = "This value must be greater then 0";

        public VolumetricValidator()
        {
            RuleFor(x => x.PreloadVolume).GreaterThan(0m).WithMessage(lessThenZeroMessage)
                .NotNull().WithMessage(notNullMessage)
                .NotEmpty().WithMessage(notNullMessage);
        }
    }
}
