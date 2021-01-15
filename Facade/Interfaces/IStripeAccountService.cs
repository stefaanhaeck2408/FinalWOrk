using Businessmodels.DTO_S;
using Businessmodels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facade.Interfaces
{
    public interface IStripeAccountService
    {
        Response<StripeAccountDTO> GetById(string accoundId);
        Response<StripeAccountDTO> Create(StripeAccountDTO stripeAccountDTO);
        Response<StripeAccountDTO> Update(StripeAccountDTO stripeAccountDTO);
        Response<StripeAccountDTO> GetByEmail(string email);

    }
}
