using ComponentUtil.Webs.Controllers;
using ComponentWebApi.Services.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComponentWebApi.Api.Controllers
{
    /// <summary>
    /// 身份验证控制器
    /// </summary>
    public class AuthenticationController : WebApiControllerBase
    {
        private readonly IAuthenticateService _authenticateService;

        /// <summary>
        /// 身份验证控制器
        /// </summary>
        /// <param name="authenticateService"></param>
        public AuthenticationController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        /// <summary>
        ///     通过RefreshToken刷新AccessToken
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("RefreshToken")]
        public IActionResult RefreshToken([FromQuery] string accessToken, [FromQuery] string refreshToken)
        {
            var result = _authenticateService.RefreshToken(refreshToken, accessToken, out var newAccessToken);
            return result.result ? Success(new {accessToken = newAccessToken}) : Fail("刷新失败");
        }
    }
}