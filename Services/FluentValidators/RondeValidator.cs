using Businessmodels.DTO_S;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.FluentValidators
{
    public class RondeValidator : AbstractValidator<RondeDTO>
    {
        public RondeValidator() {
            RuleFor(v => v.Id).NotNull().NotEqual(0).WithMessage("id mag niet leeg zijn");
            RuleFor(v => v.Naam).NotNull().NotEmpty().WithMessage("Naam mag niet leeg zijn");

        }
    }

    public class RondeRequestValidator : AbstractValidator<RondeDTO>
    {
        public RondeRequestValidator()
        {
            RuleFor(v => v.Naam).NotNull().NotEmpty().WithMessage("Naam mag niet leeg zijn");
        }
    }
}
