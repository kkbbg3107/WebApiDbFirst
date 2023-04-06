using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using WebApiDBFirst.AuthorizationFilter;
using WebApiDBFirst.Controllers.InterfaceService;
using WebApiDBFirst.Models;
using WebApiDBFirst.Repository;
using WebApiDBFirst.Services;
using WebApiDBFirst.Services.InterfaceRepository;

namespace WebApiDBFirst
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();          
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //cookie驗證註冊
            //builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(options =>
            //    {
            //        // 未登入到這
            //        options.LoginPath = new PathString("/api/Logins/NoLogin");

            //        // 沒權限到這
            //        options.AccessDeniedPath = new PathString("/api/Logins/NoAccess");

            //        //帳密有效時間
            //        options.ExpireTimeSpan = TimeSpan.FromSeconds(100);                
            //    });

            //jwt驗證註冊
            //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //.AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidIssuer = builder.Configuration.GetValue<string>("Jwt:Issuer"),
            //        ValidateAudience = true,
            //        ValidAudience = builder.Configuration.GetValue<string>("Jwt:Audience"),
            //        ValidateLifetime = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Jwt:Key")))
            //    };
            //});

            //builder.Services.AddAuthorization();

            var json = builder.Configuration.AddJsonFile("appsettings.json");
            builder.Services.AddDbContext<TodoListContext>
                (options => options.UseSqlServer(json.Build().GetConnectionString("TodoDatabase")));

            builder.Services.AddScoped<IRepository, EmployeeRepository>();
            builder.Services.AddScoped<IService, EmployeeService>();
            builder.Services.AddMvc(options =>
            {
                options.Filters.Add(new JwtAuthorizationFilter(builder.Configuration));
                options.Filters.Add(typeof(LogFilter));
                options.Filters.Add(new TodoResultFilter());
            });

            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCookiePolicy();
            app.UseAuthentication();        
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}