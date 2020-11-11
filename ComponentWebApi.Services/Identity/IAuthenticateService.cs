using System.Collections.Generic;
using System.Threading.Tasks;
using ComponentWebApi.Model.Identity;

namespace ComponentWebApi.Services.Identity
{
    public interface IAuthenticateService
    {
        /// <summary>
        ///     通过RefreshToken和已过期的AccessToken刷新AccessToken
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="refreshToken"></param>
        /// <param name="accessToken">已过期的AccessToken</param>
        /// <param name="newAccessToken">新的AccessToken</param>
        /// <returns></returns>
        (bool result, string msg) RefreshToken(string refreshToken, string accessToken, out string newAccessToken);

        /// <summary>
        ///     获取Token
        /// </summary>
        /// <param name="request">登录信息</param>
        /// <param name="userId">用户ID</param>
        /// <param name="userNickname">用户昵称</param>
        /// <returns></returns>
        Task<(bool result, string accessToken, string refreshToken)> GetToken(LoginRequestDTO request, string userId, string userNickname);

        /// <summary>
        ///     解析Token中的Claim信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Dictionary<string, string> GetClaimInfoFromToken(string token);
    }
}