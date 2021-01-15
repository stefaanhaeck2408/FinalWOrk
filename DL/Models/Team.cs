using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Models
{
    public class Team : BaseClass<int>
    {

        public string Naam { get; set; }
        public string Email { get; set; }
        public string EmailCreator { get; set; }
        public int QuizId { get; set; }
        public string PIN { get; set; }
        public virtual ICollection<IngevoerdAntwoord> IngevoerdAntwoorden { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool TeamPaidAllready { get; set; }
        public Team() 
        {
            UpdatedAt = DateTime.Now;

        }
    }
}
