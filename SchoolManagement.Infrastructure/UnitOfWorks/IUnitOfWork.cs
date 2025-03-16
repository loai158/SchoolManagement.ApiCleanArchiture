using Microsoft.EntityFrameworkCore.Storage;
using SchoolManagement.Infrastructure.InfrastructureBases;

namespace SchoolManagement.Infrastructure.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepositryAsync<T> Repositry<T>() where T : class;
        public Task<IDbContextTransaction> BeginTransactionAsync();
        public Task CommitTransactionAsync();

        public Task RollbackTransactionAsync();
        Task<int> CompleteAsync();
    }
}
