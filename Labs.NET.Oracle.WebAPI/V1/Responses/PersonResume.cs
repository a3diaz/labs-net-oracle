using Labs.NET.Oracle.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labs.NET.Oracle.WebAPI.V1.Responses
{
    public class PersonResume
    {
        public Guid PersonId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
