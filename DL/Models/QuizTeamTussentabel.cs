using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DL.Models
{
    public class QuizTeamTussentabel : BaseClass<int>
    {
        [ForeignKey("Quiz")]
        public int QuizId { get; set; }
        public virtual Quiz Quiz { get; set; }

        [ForeignKey("Team")]
        public int TeamId { get; set; }        
        public virtual Team Team { get; set; }
        public DateTime UpdatedAt { get; set; }

        public QuizTeamTussentabel() 
        {
            UpdatedAt = DateTime.Now;

        }
    }
}
