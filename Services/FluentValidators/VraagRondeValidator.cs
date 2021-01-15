using Businessmodels.DTO_S;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.FluentValidators
{
    public class VraagRondeValidator : AbstractValidator<AddVraagToRondeDTO>
    {
        public VraagRondeValidator() {
            RuleFor(AQ => AQ.RondeId).NotNull().NotEqual(0).WithMessage("Ronde Id mag niet 0 zijn");
            RuleFor(AQ => AQ.VraagId).NotNull().NotEqual(0).WithMessage("Vraag Id mag niet 0 zijn");
        }
    }
}
