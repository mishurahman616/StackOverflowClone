using NHibernate;

namespace StackOverflow.DAL.UnitOfWorks
{
    public class UnitOfWorkBase : IUnitOfWorkBase
    {
        private readonly ISession _session;
        private readonly ITransaction _transaction;

        public UnitOfWorkBase(ISession session)
        {
            _session = session;
            _transaction = _session.BeginTransaction();
        }

        public async Task BeginTransaction()
        {
            await Task.Run(() => _transaction.Begin());
        }

        public async Task Commit()
        {
            await _transaction.CommitAsync();
        }

        public async Task Rollback()
        {
            await _transaction.RollbackAsync();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _session?.Dispose();
        }
    }
}
