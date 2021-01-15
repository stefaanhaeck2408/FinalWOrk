using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Models
{
    public class StripeAccount : BaseClass<int>
    {
        public string UserEmail { get; set; }
        public string StripeAccountId { get; set; }
    }
}
