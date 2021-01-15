using Businessmodels.DTO_S;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.FluentValidators
{
    public class VerbeterValidator : AbstractValidator<VerbeterDTO>
    {
        public VerbeterValidator() {
            RuleFor(v => v.Id).NotNull();
            RuleFor(v => v.IngevoerdAntwoordId).NotNull();
            RuleFor(v => v.JsonAntwoord).NotNull().NotEmpty();
            RuleFor(v => v.JsonIngevoerdAntwoordTeam).NotNull().NotEmpty();
            RuleFor(v => v.MaxScore).NotNull().NotEmpty();
        }
    }
}
