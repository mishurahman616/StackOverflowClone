using NHibernate;
using NHibernate.Linq;
using StackOverflow.DAL.Entities.Base;
using StackOverflow.DAL.Repositories.Interfaces;
using System.Linq.Expressions;

namespace StackOverflow.DAL.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey> where TKey : IComparable
    {
        private readonly ISession _session;
        public Repository(ISession session) 
        {
            _session = session;
        }

        public async Task Create(TEntity entity)
        {
            await _session.SaveAsync(entity);
        }

        public async Task Delete(TEntity entity)
        {
            await _session.DeleteAsync(entity);
        }

        public async Task<IList<TEntity>> GetAll()
        {
           return await _session.Query<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetById(TKey id)
        {
            return await _session.GetAsync<TEntity>(id);
        }

        public async Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> predicate)
        {
            return await _session.Query<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task Update(TEntity entity)
        {
            await _session.UpdateAsync(entity);
        }
    }
}
