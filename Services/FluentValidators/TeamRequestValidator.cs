using Businessmodels.DTO_S;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Services.FluentValidators
{
    public class TeamRequestValidator: AbstractValidator<TeamDTO>
    {
        public TeamRequestValidator()
        {
            RuleFor(T => T.Naam).NotNull().NotEmpty().WithMessage("Naam mag niet leeg zijn");
            RuleFor(T => T.Email).NotNull().NotEmpty().EmailAddress().WithMessage("Er moet een email adres ingegeven zijn");
        }
    }
}
