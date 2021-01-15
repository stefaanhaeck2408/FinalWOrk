using Businessmodels.DTO_S;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.FluentValidators
{
    public class QuizRondeValidator : AbstractValidator<QuizRondeDTO>
    {
        public QuizRondeValidator()
        {
            RuleFor(q => q.QuizId).NotNull().NotEmpty().WithMessage("QuizId is Required (musn't be null or empty).");
            RuleFor(q => q.RondeId).NotNull().NotEmpty().WithMessage("RondeId is Required (musn't be null or empty).");
        }
    }
}
