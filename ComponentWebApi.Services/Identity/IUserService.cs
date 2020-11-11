using System;
using System.Threading.Tasks;
using ComponentWebApi.Model.Identity;

namespace ComponentWebApi.Services.Identity
{
    public interface IUserService : IBaseService<User, int>
    {
        /// <summary>
        ///     验证账号密码
        /// </summary>
        /// <param name="request">账号密码信息</param>
        /// <returns></returns>
        Task<(bool success, User user)> VerifyUser(LoginRequestDTO request);

        /// <summary>
        ///     检查用户名是否重名，重名返回false
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<bool> CheckUsername(string username);

        /// <summary>
        /// 根据用户名获取用户
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<User> GetUserByUsername(string username);
    }
}