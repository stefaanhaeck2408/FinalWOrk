using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Models
{
    public class Ronde : BaseClass<int>
    {
        public string Naam { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Ronde()
        {
            UpdatedAt = DateTime.Now;

        }

    }
}
