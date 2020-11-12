using MoOnlineStore.Core.EntityClasses;
using MoOnlineStore.Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoOnlineStore.Infrastructure.DataBase
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;
        private Hashtable _repositories;
        public UnitOfWork(StoreContext context)
        {
            _context = context;
        }

        public async Task<int> complete()
        {
            return await _context.SaveChangesAsync();
        }

        public async void Dispose()
        {
           await _context.DisposeAsync();      
         }

       public IGenericRepository<TEntity> repositorys<TEntity>() where TEntity : BaseEntity
        {
            if(_repositories == null) _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<TEntity>) _repositories[type];
        }
    }
}
