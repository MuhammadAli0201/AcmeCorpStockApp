using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeCorpStockApp.Models
{
    public class ResultState
    {
        public string Message { get; set; }
        public ResultStatus Status { get; set; }
    }
    public enum ResultStatus
    {
        Succeeded,
        Error,
        Warning
    }
}
