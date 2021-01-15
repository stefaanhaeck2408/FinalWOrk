using System;
using System.Collections.Generic;
using System.Text;
using Businessmodels.DTO_S;
using FluentValidation;

namespace Services.FluentValidators
{
    public class IngevoerdAntwoordValidator: AbstractValidator<IngevoerdAntwoordDTO>
    {
        public IngevoerdAntwoordValidator() {
            RuleFor(IA => IA.Id).NotNull();
            RuleFor(IA => IA.GescoordeScore).NotNull();
            RuleFor(IA => IA.JsonAntwoord).NotNull().NotEmpty();
            RuleFor(IA => IA.TeamId).NotNull().NotEqual(0).WithMessage("Team Id mag niet 0 zijn");
            RuleFor(IA => IA.VraagId).NotNull().NotEqual(0).WithMessage("Vraag Id mag niet 0 zijn");

        }
    }
}
