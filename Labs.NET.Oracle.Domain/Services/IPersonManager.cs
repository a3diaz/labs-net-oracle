using Labs.NET.Oracle.Catalogs;
using Labs.NET.Oracle.Domain.Models;
using Labs.NET.Oracle.Domain.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Labs.NET.Oracle.Domain.Services
{
    public interface IPersonManager
    {
        public Task<OperationResult<Person>> Add(string name, string lastname, Gender gender, DateTime birthDate);
        public Task<Person> Get(Guid personId);
        public Task<IList<Person>> GetAll();
        public Task<OperationResult<Person>> Update(Guid personId, string newName = null, string newLastname = null, Gender? newGender = null, DateTime? newBirthDate = null);
        public Task<OperationResult<Person>> Remove(Guid personId);
    }
}
