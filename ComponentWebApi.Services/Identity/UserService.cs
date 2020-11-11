using System;
using System.Linq;
using System.Threading.Tasks;
using ComponentWebApi.Common.Helper;
using ComponentWebApi.Model.Identity;
using ComponentWebApi.Repository.Repositories;
using ComponentWebApi.Repository.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace ComponentWebApi.Services.Identity
{
    public class UserService : BaseService<User, int>, IUserService
    {
        public async Task<(bool success, User user)> VerifyUser(LoginRequestDTO request)
        {
            var psw = PasswordHelper.UserPswEncrypt(request.Password, true);
            var user = await _repository.GetAll().FirstOrDefaultAsync(u =>
                string.Equals(u.Username, request.Username, StringComparison.CurrentCultureIgnoreCase) &&
                u.Password == psw);

            return (user != null, user);
        }

        public async Task<bool> CheckUsername(string username)
        {
            return !await _repository.GetAll().AnyAsync(user =>
                string.Equals(user.Username, username, StringComparison.CurrentCultureIgnoreCase));
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _repository.GetAll().FirstOrDefaultAsync(user =>
                string.Equals(user.Username, username, StringComparison.CurrentCultureIgnoreCase));
        }

        public UserService(IUnitOfWork unitOfWork, IRepository<User, int> repository) : base(unitOfWork, repository)
        {
        }
    }
}