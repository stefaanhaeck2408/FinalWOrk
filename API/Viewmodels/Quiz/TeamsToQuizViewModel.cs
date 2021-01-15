using API.Viewmodels.Team;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Viewmodels.Quiz
{
    public class TeamsToQuizViewModel
    {
        public int QuizId { get; set; }
        public IEnumerable<TeamViewModelResponse> TeamsInQuiz { get; set; }
    }
}
