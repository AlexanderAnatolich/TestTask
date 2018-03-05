using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Create(TEntity item);
        Task CreateAsync(TEntity item);

        TEntity FindById(int? id);
        Task<TEntity> FindByIdAsync(int? id);

        IEnumerable<TEntity> Get();
        Task<List<TEntity>> GetAsync();

        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
        Task<IEnumerable<TEntity>> GetAsync(Func<TEntity, bool> predicate);

        void Remove(TEntity item);
        Task RemoveAsync(TEntity item);
        void Update(TEntity item);
        Task UpdateAsync(TEntity item);
    }
}
