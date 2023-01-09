using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EnginCan.Core.Helpers.Handlers
{
    public class ErrorHandler
    {
        private readonly RequestDelegate next;

        public ErrorHandler(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Global error Handler
        /// TODO: May be store request body and header
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <param name="systemErrorRepository"></param>
        /// <returns></returns>
        public static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var result = JsonConvert.SerializeObject(new { error = ex.Message });

            // Only save release
            //if (!Debugger.IsAttached)
            //    _logger.LogError(ex);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(result);
        }
    }
}