using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DL.Models
{
    public abstract class BaseClass<TKey> where TKey : struct
    {
        [Key]
        public TKey Id { get; set; }

    }
}
