using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DL.Models
{
    public class RondeVraagTussentabel : BaseClass<int>
    {
        [ForeignKey("Ronde")]
        public int RondeId { get; set; }
        public virtual Ronde Ronde { get; set; }

        [ForeignKey("Vraag")]
        public int VraagId { get; set; }
        public virtual Vraag Vraag { get; set; }
        public DateTime UpdatedAt { get; set; }

        public RondeVraagTussentabel()
        {
            UpdatedAt = DateTime.Now;

        }
    }
}
