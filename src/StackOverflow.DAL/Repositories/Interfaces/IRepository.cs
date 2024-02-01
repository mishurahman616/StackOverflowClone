using StackOverflow.DAL.Entities.Base;

namespace StackOverflow.DAL.Repositories.Interfaces
{
    public interface IRepository<TEntity, TKey> where TEntity : IEntity<TKey> 
        where TKey : IComparable
    {
        Task Create(TEntity entity);
        Task Delete(TEntity entity);
        Task Update(TEntity entity);
        Task<TEntity> GetById(TKey id);
        Task<IList<TEntity>> GetAll();
    }
}
