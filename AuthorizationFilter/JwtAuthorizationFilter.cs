using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace WebApiDBFirst.AuthorizationFilter
{
    public class JwtAuthorizationFilter: IAuthorizationFilter
    {
        private readonly IConfiguration _configuration;

        public JwtAuthorizationFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var ignore = context.ActionDescriptor.EndpointMetadata
                .Where(x => x.GetType() == typeof(AllowAnonymousAttribute))
                .Select(x => x).FirstOrDefault();
            var token = string.Empty;
            // 要驗證的api
            if (ignore is null)
            {
                // 取得Authorization標頭
                var authorizationHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

                if (authorizationHeader == null || !authorizationHeader.StartsWith("Bearer "))
                {
                    // 如果Authorization標頭不存在或不是Bearer驗證方式，返回401 Unauthorized狀態碼
                    context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
                    return;
                }

                // 從Authorization標頭中取得JWT token
                token = authorizationHeader.Substring("Bearer ".Length).Trim();
                try
                {
                    // 解析JWT token
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var jwtSecretKey = _configuration.GetValue<string>("Jwt:Key");
                    var jwtIssuer = _configuration.GetValue<string>("Jwt:Issuer");
                    var jwtAudience = _configuration.GetValue<string>("Jwt:Audience");
                    var validationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtIssuer,

                        ValidateAudience = true,
                        ValidAudience = jwtAudience,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtSecretKey)),

                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                    };            

                    var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);                   
                    //// 將驗證通過的用戶上下文存放在HttpContext中，以便後續控制器使用
                    context.HttpContext.Items["User"] = principal;
                }
                catch (Exception)
                {
                    // 如果JWT token驗證失敗，返回401 Unauthorized狀態碼
                    context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
                    return;
                }
            }            
        }
    }
}
