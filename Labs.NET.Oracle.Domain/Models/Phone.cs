using Labs.NET.Oracle.Catalogs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Labs.NET.Oracle.Domain.Models
{
    public sealed class Phone
    {
        public Guid PhoneId { get; set; }
        public string Number { get; set; }
        public Guid OwnerId { get; set; }
        public PhoneType Type { get; set; }
        public Person Owner { get; set; }
    }
}
