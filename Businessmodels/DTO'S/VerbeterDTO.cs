using System;
using System.Collections.Generic;
using System.Text;

namespace Businessmodels.DTO_S
{
    public class VerbeterDTO
    {
        public int Id { get; set; }
        public string JsonAntwoord { get; set; }
        public string JsonIngevoerdAntwoordTeam { get; set; }
        public int IngevoerdAntwoordId { get; set; }
        public int MaxScore { get; set; }

    }
}
