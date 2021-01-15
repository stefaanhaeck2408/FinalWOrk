using System;
using System.Collections.Generic;
using System.Text;

namespace Businessmodels.DTO_S
{
    public class TeamDTO
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Email { get; set; }
        public string EmailCreator { get; set; }
        public int QuizId { get; set; }
        public string PIN { get; set; }
        public ICollection<IngevoerdAntwoordDTO> IngevoerdAntwoordenDTO { get; set; }
        public bool TeamPaidAllready { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
