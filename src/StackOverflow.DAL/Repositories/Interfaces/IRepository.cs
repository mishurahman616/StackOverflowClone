using StackOverflow.DAL.Entities.Base;
using System.Linq.Expressions;

namespace StackOverflow.DAL.Repositories.Interfaces
{
    public interface IRepository<TEntity, TKey> where TEntity : IEntity<TKey> 
        where TKey : IComparable
    {
        Task Create(TEntity entity);
        Task Delete(TEntity entity);
        Task Update(TEntity entity);
        Task<TEntity> GetById(TKey id);
        Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> filters);
        Task<IList<TEntity>> GetAll();
    }
}
