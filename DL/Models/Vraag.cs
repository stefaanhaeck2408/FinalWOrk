using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DL.Models
{
    public class Vraag : BaseClass<int>
    {        
        public string VraagStelling { get; set; }
        public int MaxScoreVraag { get; set; }       
        public string JsonCorrecteAntwoord { get; set; }
        public string JsonMogelijkeAntwoorden { get; set; }

        [ForeignKey("TypeVraag")]
        public int TypeVraagId { get; set; }
        public virtual TypeVraag TypeVraag { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Vraag()
        {
            UpdatedAt = DateTime.Now;

        }
    }
}
