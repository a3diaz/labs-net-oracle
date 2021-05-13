using Labs.NET.Oracle.Catalogs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Labs.NET.Oracle.Domain.Models
{
    public sealed class Person
    {
        public Guid PersonId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Phone> Phones { get; set; }
    }
}
