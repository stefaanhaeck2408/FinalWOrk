using Businessmodels.DTO_S;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.FluentValidators
{
    public class TeamValidator : AbstractValidator<TeamDTO>
    {
        public TeamValidator() {
            RuleFor(T => T.Id).NotNull().NotEqual(0).WithMessage("Id mag niet 0 zijn");
            RuleFor(T => T.Naam).NotNull().NotEmpty().WithMessage("Naam mag niet leeg zijn");
            RuleFor(T => T.Email).NotNull().NotEmpty().WithMessage("Email mag niet leeg zijn");
            RuleFor(T => T.EmailCreator).NotNull().NotEmpty().WithMessage("Email creator mag niet leeg zijn");
    }
    }
}
