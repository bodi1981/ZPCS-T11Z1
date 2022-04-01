using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinances.WebApi.Models.Response
{
    public class Error
    {
        public Error(string source, string message)
        {
            Source = source;
            Message = message;
        }
        public string Source { get; set; }
        public string Message { get; set; }
    }
}
