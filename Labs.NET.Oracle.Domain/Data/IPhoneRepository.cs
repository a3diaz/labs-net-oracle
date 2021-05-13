using Labs.NET.Oracle.Catalogs;
using Labs.NET.Oracle.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Labs.NET.Oracle.Domain.Data
{
    public interface IPhoneRepository
    {
        Task Insert(Phone phone);
        Task<bool> Exists(string number);
        Task<bool> Exists(Guid phoneId);
        Task<Phone> SelectFirst(Func<Phone, bool> predicate);
        Task<IList<Phone>> SelectAll(Func<Phone, bool> predicate);
        Task Update(Phone phone);
        Task Update(IEnumerable<Phone> phone);
        Task Delete(Guid phoneId);
    }
}
