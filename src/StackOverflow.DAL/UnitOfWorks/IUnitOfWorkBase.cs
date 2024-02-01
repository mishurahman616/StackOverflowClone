namespace StackOverflow.DAL.UnitOfWorks
{
    public interface IUnitOfWorkBase : IDisposable
    {
        Task BeginTransaction();
        Task Commit();
        Task Rollback();
    }
}
