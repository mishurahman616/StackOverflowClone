using StackOverflow.DAL.Entities;

namespace StackOverflow.BL.Services
{
    public interface IUserService
    {
        public Task<User> GetUserById(Guid id);
        public Task<User>  GetUserByUsername(string username);
    }
}
