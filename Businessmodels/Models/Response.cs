using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Businessmodels.Models
{
    public class Response<TDTO>
    {
        public ICollection<Error> Errors { get; set; }
        public TDTO DTO { get; set; }

        public bool DidError => Errors != null && Errors.Count() > 0;
    }

    public class Error
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ErrorType Type { get; set; }
        public string Message { get; set; }
    }

    public enum ErrorType
    {        
        ValidationError,
        BussinessError,
        Exception,
        NotFound
    }
}
