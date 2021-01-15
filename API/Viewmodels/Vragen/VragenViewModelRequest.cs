using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Viewmodels.Vragen
{
    public class VragenViewModelRequest
    {
        //public int Id { get; set; }
        public int MaxScoreVraag { get; set; }
        public int TypeVraagId { get; set; }
        public string VraagStelling { get; set; }
        public string JsonCorrecteAntwoord { get; set; }
        public string JsonMogelijkeAntwoorden { get; set; }
    }
}
