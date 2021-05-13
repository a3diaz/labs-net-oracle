using Labs.NET.Oracle.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Labs.NET.Oracle.Domain.Data
{
    public interface IPersonRepository
    {
        Task Insert(Person person);
        Task Insert(IEnumerable<Person> persons);
        Task Delete(Guid personId);
        Task Update(Person person);
        Task<bool> Exists(Guid personId);
        Task<int> CountAll();
        Task<Person> SelectFirst(Guid personId);
        Task<IList<Person>> SelectAll(int skip = 0, int take = 100);
    }
}
