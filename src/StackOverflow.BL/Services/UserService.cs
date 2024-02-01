using StackOverflow.DAL.Entities;
using StackOverflow.DAL.UnitOfWorks;

namespace StackOverflow.BL.Services
{
    public class UserService: IUserService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;

        public UserService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await _unitOfWork.Users.GetById(id);
        }

        public async Task<User> GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }
    }
}
