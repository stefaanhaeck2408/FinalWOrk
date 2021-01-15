using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Viewmodels
{
    public class QuizViewModelResponse
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool FreeQuiz { get; set; }
        public bool DidCreatorPayAllready { get; set; }
        public string EmailCreator { get; set; }
        public string EntryFee { get; set; }
    }
}
