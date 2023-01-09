using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EnginCan.Core.Helpers.Attributes
{
    /// <summary>
    /// Gelen isteklerin sadece yerel bir kaynaktan gelmesini sağlar.
    /// </summary>
    public class IsLocalRequestAttribute : TypeFilterAttribute
    {
        public IsLocalRequestAttribute() : base(typeof(MethodSecurityAttributeImpl))
        {
        }

        private class MethodSecurityAttributeImpl : IActionFilter
        {
            public void OnActionExecuting(ActionExecutingContext context)
            {
                string remoteAddress = context.HttpContext.Connection.RemoteIpAddress.ToString();

                if (remoteAddress != "127.0.0.1" & remoteAddress != "::1")
                {
                    context.Result = new ForbidResult();
                    return;
                }
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
            }
        }
    }
}