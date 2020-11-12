using MoOnlineStore.Core.EntityClasses;
using System;
using System.Threading.Tasks;

namespace MoOnlineStore.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> repositorys<TEntity>() where TEntity : BaseEntity;
        Task<int> complete();

    }
}
