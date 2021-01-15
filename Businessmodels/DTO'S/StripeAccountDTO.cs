using System;
using System.Collections.Generic;
using System.Text;

namespace Businessmodels.DTO_S
{
    public class StripeAccountDTO
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string StripeAccountId { get; set; }
    }
}
