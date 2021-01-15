using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Viewmodels.Team
{
    public class TeamViewModelResponse
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public int QuizId { get; set; }
        public string PIN { get; set; }
        public string Email { get; set; }
        public string EmailCreator { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool TeamPaidAllready { get; set; }
    }
}
