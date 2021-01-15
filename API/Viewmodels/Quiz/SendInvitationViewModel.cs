using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Viewmodels.Quiz
{
    public class SendInvitationViewModel
    {
        public int QuizId { get; set; }
        public string Message { get; set; }
        public string PaypalLink { get; set; }
    }
}
