using Businessmodels.DTO_S;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.FluentValidators
{
    public class RondeQuizValidator : AbstractValidator<AddRondeToQuizDTO>
    {
        public RondeQuizValidator() {
            RuleFor(AQ => AQ.QuizId).NotNull().NotEqual(0).WithMessage("Quiz Id mag niet 0 zijn");
            RuleFor(AQ => AQ.RondeId).NotNull().NotEqual(0).WithMessage("TeamId mag niet 0 zijn");
        }
    }
}
