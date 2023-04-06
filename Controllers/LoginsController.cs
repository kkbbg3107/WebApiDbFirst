using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiDBFirst.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WebApiDBFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LoginsController : ControllerBase
    {
        private readonly TodoListContext _context;

        private readonly IConfiguration _configuration;
        public LoginsController(TodoListContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/Logins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Login>>> GetLogins()
        {
          if (_context.Logins == null)
          {
              return NotFound();
          }
            return await _context.Logins.ToListAsync();
        }
      
        [HttpGet("NoLogin")]
        public async Task<ActionResult<string>> GetLogin()
        {
            return "未登入";
        }
        
        // POST: api/Logins
        [HttpPost("CookieLogin")]
        public async Task<ActionResult<string>> CookieLogin(Login login)
        {
            var user = (from a in _context.Logins
                        where a.Account == login.Account
                        && a.Password == login.Password
                        select a).SingleOrDefault();

            if (user is null)
            {
                return "帳號或密碼錯誤";
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Account),
                    new Claim("FullName", user.MemberName),
                    new Claim("EmployeeId", user.Id.ToString()),
                };

                //foreach (var item in _context.Roles)
                //{
                //    claims.Add(new Claim(ClaimTypes.Role, item.Name));              
                //}
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return "ok";
            }
        }

        // POST: api/Logins/JwtLogin
        [HttpPost("JwtLogin")]
        public async Task<ActionResult<string>> JwtLogin(Login login)
        {
            var user = (from a in _context.Logins
                        where a.Account == login.Account
                        && a.Password == login.Password
                        select a).SingleOrDefault();

            if (user is null)
            {
                return "帳號或密碼錯誤";
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Email, user.Account),
                    new Claim("FullName", user.MemberName),
                    new Claim(JwtRegisteredClaimNames.Name, user.MemberName),
                };

                var secureKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                //設定jwt相關資訊
                var jwt = new JwtSecurityToken
                (
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: new SigningCredentials(secureKey, SecurityAlgorithms.HmacSha256)
                );
                //產生JWT Token
                var token = new JwtSecurityTokenHandler().WriteToken(jwt);

                //回傳JWT Token給認證通過的使用者
                return token;
            }
        }


        // DELETE: api/Logins/5
        [HttpDelete]
        public string LoginOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return "已登出";
        }
    }
}
