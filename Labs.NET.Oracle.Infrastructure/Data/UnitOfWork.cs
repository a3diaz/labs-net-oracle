using Labs.NET.Oracle.Domain.Data;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace Labs.NET.Oracle.Infrastructure.Data
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly LabsContext _labsContext;
        private IDbContextTransaction _transaction;
        public UnitOfWork(LabsContext labsContext)
        {
            _labsContext = labsContext;
        }
        public async Task BeginTransaction()
        {
            if (_transaction != null)
                throw new InvalidOperationException("A transaction for this unit of work already exists.");

            _transaction = await _labsContext.Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            if (_transaction is null)
                throw new InvalidOperationException("This unit of work already does not have an active transaction.");

            await _transaction.CommitAsync();
            _transaction = default;
        }

        public void Dispose()
        {
            _transaction?.Dispose();
        }

        public async Task Save()
        {
            await _labsContext.SaveChangesAsync();
        }

        public async Task SaveAndCommit()
        {
            if (_transaction is null)
                throw new InvalidOperationException("This unit of work already does not have an active transaction.");

            await _labsContext.SaveChangesAsync();
            await _transaction.CommitAsync();
            _transaction = default;
        }
    }
}
