using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Models
{
    public class Quiz : BaseClass<int>
    {
     
        public string Naam { get; set; }
        public string EmailCreator { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool FreeQuiz { get; set; }
        public int EntryFee { get; set; }
        public Quiz()
        {
            UpdatedAt = DateTime.Now;        
        }


    }
}
