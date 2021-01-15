using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DL.Models
{
    public class IngevoerdAntwoord : BaseClass<int>
    {
       
        public string JsonAntwoord { get; set; }

        public int GescoordeScore { get; set; }

        [ForeignKey("Vraag")]
        public int VraagId { get; set; }
        public virtual Vraag Vraag { get; set; }

        [ForeignKey("Team")]
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
        public DateTime UpdatedAt { get; set; }

        public IngevoerdAntwoord()
        {
            UpdatedAt = DateTime.Now;
        }


    }
}
