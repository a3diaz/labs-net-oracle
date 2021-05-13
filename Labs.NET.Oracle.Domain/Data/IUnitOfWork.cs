using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Labs.NET.Oracle.Domain.Data
{
    public interface IUnitOfWork : IDisposable
    {
        public Task BeginTransaction();
        public Task Save();
        public Task SaveAndCommit();
        public Task Commit();
    }
}
