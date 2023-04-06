using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiDBFirst.AuthorizationFilter
{
    public class TodoResultFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {            
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            var contextResult = context.Result as ObjectResult;

            if (context.ModelState.IsValid)
            {
                context.Result = new JsonResult(new ResponseType()
                {
                    Data = contextResult.Value,
                    HttpCode = context.HttpContext.Response.StatusCode,
                    ErrorMessage = "",
                });
            }
            else
            {
                context.Result = new JsonResult(new ResponseType()
                {
                    Data = "",
                    HttpCode = context.HttpContext.Response.StatusCode,
                    ErrorMessage = contextResult.Value,
                });
            }            
        }
    }
}
