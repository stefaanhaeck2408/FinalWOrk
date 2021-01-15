using System;
using System.Collections.Generic;
using System.Text;

namespace Businessmodels.DTO_S
{
    public class IngevoerdAntwoordDTO
    {
        public int Id { get; set; }
        public int GescoordeScore { get; set; }
        public string JsonAntwoord { get; set; }

        public int TeamId { get; set; }
        public int VraagId { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
