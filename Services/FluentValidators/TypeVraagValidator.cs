using Businessmodels.DTO_S;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.FluentValidators
{
    public class TypeVraagValidator : AbstractValidator<TypeVraagDTO>
    {
        public TypeVraagValidator() {
            RuleFor(TV => TV.Id).NotNull();
            RuleFor(TV => TV.Naam).NotNull().NotEmpty();
        }
    }   
}
