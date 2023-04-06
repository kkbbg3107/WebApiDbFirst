using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace WebApiDBFirst.AuthorizationFilter
{
    public class LogFilter : Attribute, IActionFilter
    {
        private IWebHostEnvironment _env;

        public LogFilter(IWebHostEnvironment env)
        {
            _env = env;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            string rootPath = _env.ContentRootPath + @"\Log\";
            var data = context.HttpContext.Request;

            var path = data.Path;
            var method = data.Method;
            var userEmail = context.HttpContext.User.FindFirst(ClaimTypes.Email);
            var text = $"結束 : {DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} \n path :{path} \n userEmail : {userEmail} \n method : {method}";
            File.AppendAllText(rootPath + DateTime.Now.ToString("yyyyMMdd") + ".txt", text);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string rootPath = _env.ContentRootPath + @"Log\";
            var file = rootPath + DateTime.Now.ToString("yyyyMMdd") + ".txt";

            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }
            var data = context.HttpContext.Request;

            var path = data.Path;
            var method = data.Method;
            var userEmail = context.HttpContext.User.FindFirst("FullName");
            var text = $"開始 : {DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} \n path :{path} \n userEmail : {userEmail} \n method : {method} \n";
            File.AppendAllText(rootPath + DateTime.Now.ToString("yyyyMMdd") + ".txt", text);
        }
    }
}
