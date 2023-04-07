using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Runtime.Intrinsics.X86;
using WebApiDBFirst.Models;

namespace WebApiDBFirst.AuthorizationFilter
{
    public class BlackListFilter : Attribute, IActionFilter
    {
        private string[] _blacklist;
        public BlackListFilter(string[] blackList = default)
        {
            _blacklist = blackList;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var parameters = context.ActionArguments.Values.ToJson().ToString();
            int startIndex = parameters.IndexOf('[') + 1;
            int endIndex = parameters.IndexOf(']');
            string innerString = parameters.Substring(startIndex, endIndex - startIndex);
            var para = JsonConvert.DeserializeObject<EmployeeDetail>(innerString);
            if (_blacklist.Contains(para.Name))
            {
                throw new Exception("此人為黑名單,已拒絕入職申請");
            }
        }
    }
}
