using MoOnlineStore.Core.DTO;
using MoOnlineStore.Core.EntityClasses;
using MoOnlineStore.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoOnlineStore.Core.Interfaces
{
   public interface IGenericRepository<T> where T: BaseEntity
    {
        Task<T> GetByID(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> ListWithSpecAsync(ISpecification<T> spec);
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
