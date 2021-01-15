using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DL.Models
{
    public class QuizRondeTussentabel : BaseClass<int>
    {
        [ForeignKey("Quiz")]
        public int QuizId { get; set; }
        public virtual Quiz Quiz { get; set; }

        [ForeignKey("Ronde")]
        public int RondeId { get; set; }
        public virtual Ronde Ronde { get; set; }
        public DateTime UpdatedAt { get; set; }

        public QuizRondeTussentabel() 
        {
            UpdatedAt = DateTime.Now;
        }
    }
}
