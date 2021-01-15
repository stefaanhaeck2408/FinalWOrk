using System;
using System.Collections.Generic;
using System.Text;

namespace Businessmodels.DTO_S
{
    public class QuizDTO
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string EmailCreator { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool FreeQuiz { get; set; }
        public string EntryFee { get; set; }
    }
}
