using System;
using System.Text;
using System.Threading.Tasks;
using ComponentWebApi.Model.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ComponentWebApi.Api.Extensions
{
    /// <summary>
    /// JWT配置
    /// </summary>
    public static class JwtSetup
    {
        /// <summary>
        /// JWT配置
        /// </summary>
        public static void AddJwtBearer(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtConfiguration>(configuration.GetSection("JwtConfiguration"));
            var tokenInfo = configuration.GetSection("JwtConfiguration").Get<JwtConfiguration>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    //验证失败时
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            context.Response.Headers.Add("act", "expired");

                        return Task.CompletedTask;
                    }
                };

                x.RequireHttpsMetadata = false;
                //x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenInfo.AccessSecret)),
                    ValidIssuer = tokenInfo.Issuer,
                    ValidAudience = tokenInfo.Audience,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(tokenInfo.ClockSkew)
                };
            });
        }
    }
}