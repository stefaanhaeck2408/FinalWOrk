using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Viewmodels.Rondes
{
    public class RondeViewModelResponse
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
