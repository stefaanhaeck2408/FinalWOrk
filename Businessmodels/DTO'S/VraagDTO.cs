using System;

namespace Businessmodels.DTO_S
{
    public class VraagDTO
    {
        public int Id { get; set; }
        public string VraagStelling { get; set; }
        public int MaxScoreVraag { get; set; }
        public string JsonCorrecteAntwoord { get; set; }
        public string JsonMogelijkeAntwoorden { get; set; }

        public int TypeVraagId { get; set; }
        public TypeVraagDTO TypeVraagDTO { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
