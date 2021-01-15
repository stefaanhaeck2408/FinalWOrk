using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Models
{
    public class TypeVraag : BaseClass<int>
    {
        public string Naam { get; set; }
        public DateTime UpdatedAt { get; set; }

        public TypeVraag()
        {
            UpdatedAt = DateTime.Now;

        }
    }
}
