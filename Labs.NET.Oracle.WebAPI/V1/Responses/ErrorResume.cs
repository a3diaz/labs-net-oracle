using Labs.NET.Oracle.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labs.NET.Oracle.WebAPI.V1.Responses
{
    public class ErrorResume
    {
        public OperationStatus OperationStatus { get; set; }
        public string Description { get; set; }
    }
}
