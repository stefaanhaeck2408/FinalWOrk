using Businessmodels.DTO_S;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.FluentValidators
{
    public class AddTeamToQuizValidator : AbstractValidator<AddTeamToQuizDTO>
    {
        public AddTeamToQuizValidator(){
            RuleFor(AQ => AQ.QuizId).NotNull().NotEqual(0);
            RuleFor(AQ => AQ.TeamId).NotNull().NotEqual(0);
        }
    }
}
