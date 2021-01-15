using Businessmodels.DTO_S;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.FluentValidators
{
    public class VraagValidation : AbstractValidator<VraagDTO>
    {
        public VraagValidation() {
            RuleFor(v => v.Id).NotNull();
            //RuleFor(v => v.JsonCorrecteAntwoord).NotNull().NotEmpty().WithMessage("Correcte antwoord mag niet leeg zijn");
            //RuleFor(v => v.JsonMogelijkeAntwoorden).NotNull().NotEmpty().WithMessage("Mogelijke antwoorden mag niet leeg zijn");
            RuleFor(v => v.MaxScoreVraag).NotNull();
            RuleFor(v => v.TypeVraagId).NotNull().NotEqual(0).WithMessage("Vraag id mag niet 0 zijn");
            RuleFor(v => v.VraagStelling).NotNull().NotEmpty().WithMessage("De vraag mag niet leeg zijn");
        }
    }
}
