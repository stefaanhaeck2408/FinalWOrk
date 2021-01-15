using Businessmodels.DTO_S;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.FluentValidators
{
    public class QuizValidator: AbstractValidator<QuizDTO>
    {
        public QuizValidator() {
            RuleFor(q => q.Id).NotNull();
            RuleFor(q => q.Naam).NotNull().NotEmpty().WithMessage("Name is Required (musn't be null or empty).");
            RuleFor(q => q.EmailCreator).NotNull().NotEmpty().WithMessage("A team must have an creator email adresse");
        }
        
    }
}
