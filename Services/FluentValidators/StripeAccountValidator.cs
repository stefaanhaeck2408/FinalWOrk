using Businessmodels.DTO_S;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.FluentValidators
{
    public class StripeAccountValidator : AbstractValidator<StripeAccountDTO>
    {
        public StripeAccountValidator()
        {
            RuleFor(a => a.UserEmail).NotNull().NotEmpty().WithMessage("Email of user is Required (musn't be null or empty).");
            RuleFor(a => a.StripeAccountId).NotNull().NotEmpty().WithMessage("Stripe account id cannot be null or empty");
        }
    }
}
