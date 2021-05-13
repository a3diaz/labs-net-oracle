using Labs.NET.Oracle.Catalogs;
using Labs.NET.Oracle.Domain.Models;
using Labs.NET.Oracle.Domain.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Labs.NET.Oracle.Domain.Services
{
    public interface IPhoneManager
    {
        public Task<OperationResult<Phone>> Assign(string number, Guid personId, PhoneType type);
        public Task<IList<Phone>> Get(Guid personId);
        public Task<OperationResult<Phone>> Update(Guid phoneId, string newNumber = null, Guid? newPersonId = null, PhoneType? newType = null);
        public Task<OperationResult<Phone>> Remove(Guid phoneId);
    }
}
