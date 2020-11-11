using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ComponentUtil.Common.Crypto;
using ComponentWebApi.Model.Base;
using ComponentWebApi.Model.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ComponentWebApi.Services.Identity
{
    public class TokenAuthenticationService : IAuthenticateService
    {
        private readonly JwtConfiguration _tokenManagement;
        private readonly IUserService _userService;

        public TokenAuthenticationService(IUserService userService, IOptions<JwtConfiguration> tokenManagement)
        {
            _userService = userService;
            _tokenManagement = tokenManagement.Value;
        }

        public (bool result, string msg) RefreshToken(string refreshToken, string accessToken,
            out string newAccessToken)
        {
            //先验证RefreshToken
            try
            {
                var handle = new JwtSecurityTokenHandler();
                handle.ValidateToken(refreshToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _tokenManagement.Issuer,
                    ValidAudience = _tokenManagement.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.RefreshSecret)),
                    ValidateLifetime = true,
                    RequireExpirationTime = true
                }, out var validatedRefreshToken);
            }
            catch (Exception e)
            {
                newAccessToken = string.Empty;
                return (false, "无效的RefreshToken");
            }

            //再验证AccessToken是否为已过期状态，若为无效Token则不予刷新
            try
            {
                var handle = new JwtSecurityTokenHandler();
                handle.ValidateToken(accessToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _tokenManagement.Issuer,
                    ValidAudience = _tokenManagement.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.AccessSecret)),
                    ValidateLifetime = true,
                    RequireExpirationTime = true
                }, out var validatedAccessToken);
            }
            //当状态为已过期时
            catch (SecurityTokenExpiredException)
            {
            }
            //其他异常视为无效Token
            catch (Exception e)
            {
                newAccessToken = string.Empty;
                return (false, "无效的AccessToken");
            }

            {
                //解析AccessToken信息
                var dic = GetClaimInfoFromToken(accessToken);
                newAccessToken = CalAccessToken(dic[ClaimTypes.Name], dic[ClaimTypes.Sid], dic[ClaimTypes.Surname]);
                return string.IsNullOrWhiteSpace(newAccessToken) ? (false, "") : (true, newAccessToken);
            }
        }

        public async Task<(bool result, string accessToken, string refreshToken)> GetToken(LoginRequestDTO request,
            string userId, string userNickname)
        {
            var result = await _userService.VerifyUser(request);
            if (!result.success) return (false, string.Empty, string.Empty);

            var accessToken = CalAccessToken(request.Username, userId, userNickname);
            var refreshToken = CalRefreshToken(request.Username, userId, userNickname);

            if (string.IsNullOrWhiteSpace(accessToken) || string.IsNullOrWhiteSpace(refreshToken))
                return (false, string.Empty, string.Empty);

            return (true, accessToken, refreshToken);
        }

        public Dictionary<string, string> GetClaimInfoFromToken(string token)
        {
            var handle = new JwtSecurityTokenHandler();
            if (handle.CanReadToken(token))
            {
                var claimStr = Base64Helper.Base64Decode(token.Split('.')[1]);
                return JsonSerializer.Deserialize<Dictionary<string, string>>(claimStr);
            }

            return null;
        }

        /// <summary>
        ///     获取RefreshToken
        /// </summary>
        /// <param name="userName">userName</param>
        /// <param name="userId">userId</param>
        /// <param name="nickName">昵称</param>
        /// <returns></returns>
        private string CalRefreshToken(string userName, string userId, string nickName)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Sid, userId),
                new Claim(ClaimTypes.Surname, nickName),
            };
            claims = new[] {new Claim(ClaimTypes.Name, userName)};
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.RefreshSecret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtRefreshToken = new JwtSecurityToken(_tokenManagement.Issuer, _tokenManagement.Audience, claims,
                expires: DateTime.Now.AddSeconds(_tokenManagement.RefreshExpiration), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtRefreshToken);
        }

        /// <summary>
        ///     获取AccessToken
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="userId">userId</param>
        /// <param name="nickName">昵称</param>
        /// <returns></returns>
        private string CalAccessToken(string userName, string userId, string nickName)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Sid, userId),
                new Claim(ClaimTypes.Surname, nickName),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.AccessSecret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtAccessToken = new JwtSecurityToken(_tokenManagement.Issuer, _tokenManagement.Audience, claims,
                expires: DateTime.Now.AddSeconds(_tokenManagement.AccessExpiration), signingCredentials: credentials,
                notBefore: DateTime.Now);

            return new JwtSecurityTokenHandler().WriteToken(jwtAccessToken);
        }
    }
}