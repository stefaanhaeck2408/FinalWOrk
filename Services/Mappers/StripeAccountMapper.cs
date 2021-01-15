using Businessmodels.DTO_S;
using DL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Mappers
{
    public class StripeAccountMapper
    {
        public static StripeAccountDTO MapStripeAccountModelToStripeAccountDTO(StripeAccount stripeAccount)
        {
            if (stripeAccount == null)
            {
                throw new NullReferenceException("stripe account object is null");
            }
            return new StripeAccountDTO
            {
                Id = stripeAccount.Id,
                StripeAccountId = stripeAccount.StripeAccountId,
                UserEmail = stripeAccount.UserEmail,

            };
        }

        public static StripeAccount MapStripeAccountDTOToStripeAccountModel(StripeAccountDTO stripeAccountDTO)
        {
            if (stripeAccountDTO == null)
            {
                throw new NullReferenceException("stripe account DTO is null");
            }
            return new StripeAccount
            {
                Id = stripeAccountDTO.Id,
                StripeAccountId = stripeAccountDTO.StripeAccountId,
                UserEmail = stripeAccountDTO.UserEmail,

            };
        }
    }
}
