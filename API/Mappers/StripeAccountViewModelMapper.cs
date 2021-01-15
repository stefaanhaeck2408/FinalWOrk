using API.Viewmodels.StripeAccount;
using Businessmodels.DTO_S;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Mappers
{
    public static class StripeAccountViewModelMapper
    {
        public static StripeAccountViewModel MapStripeAccountDTOToStripeAccount(StripeAccountDTO stripeDTO)
        {
            if (stripeDTO == null)
            {
                throw new NullReferenceException("stripe dto is null");
            }
            return new StripeAccountViewModel
            {
                StripeAccountId = stripeDTO.StripeAccountId,
                UserEmail = stripeDTO.UserEmail

            };
        }
    }
}
