using System;
using System.Collections.Generic;
using System.Text;

namespace Businessmodels.DTO_S
{
    public class QuizIngevoerdAntwoordTussentabelDTO
    {
        public int Id { get; set; }
        public int QuizDTOId { get; set; }
        //public QuizDTO QuizDTO { get; set; }
        public int VraagDTOId { get; set; }
        //public VraagDTO VraagDTO { get; set; }
    }
}
