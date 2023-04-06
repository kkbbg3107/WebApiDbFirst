using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace WebApiDBFirst.AuthorizationFilter
{
    public class TodoAuthorizationFilter :Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            
            var ignore = context.ActionDescriptor.EndpointMetadata
                .Where(x => x.GetType() == typeof(AllowAnonymousAttribute))
                .Select(x => x).FirstOrDefault();            

            if (ignore is null)
            {    
                bool tokenFlag = context.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues outValue);

                if (tokenFlag)
                {
                    if (outValue == "123")
                    {
                        context.Result = new JsonResult(new ResponseType()
                        {
                            HttpCode = 401,
                            ErrorMessage = "沒有登入"
                        });
                    }   
                }
                else
                {
                    context.Result = new JsonResult(new ResponseType()
                    {
                        HttpCode = 401,
                        ErrorMessage = "沒有登入"
                    });
                }
            }
        }
    }
}
