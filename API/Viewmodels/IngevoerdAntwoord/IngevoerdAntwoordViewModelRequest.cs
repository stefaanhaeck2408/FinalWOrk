using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Viewmodels.IngevoerdAntwoord
{
    public class IngevoerdAntwoordViewModelRequest
    {
        //public int Id { get; set; }
        public int TeamId { get; set; }
        public int VraagId { get; set; }
        public int GescoordeScore { get; set; }
        public string Antwoord { get; set; }
    }
}
