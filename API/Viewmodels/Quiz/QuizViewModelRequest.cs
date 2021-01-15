using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Viewmodels
{
    public class QuizViewModelRequest
    {
        //public int Id { get; set; }
        public string Naam { get; set; }
        public string EmailCreator { get; set; }
        public bool FreeQuiz { get; set; }
        public string EntryFee { get; set; }
    }
}
