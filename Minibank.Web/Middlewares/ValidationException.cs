using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Minibank.Core;

namespace Minibank.Web.Middlewares
{
    public class ValidationException: Exception
    {
        public readonly RequestDelegate next;

        public ValidationException(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch(UserFriendlyException exception)
            {
                httpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                await httpContext.Response.WriteAsJsonAsync(new {Message = $"Ошибка:{httpContext.Response.StatusCode}" +
                                                                           $" Сообщение:{exception.Message}"});

            }
            catch (Exception exception)
            {
                httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                await httpContext.Response.WriteAsJsonAsync(new {Message = $"Ошибка:{httpContext.Response.StatusCode}"});
            }
        }
    }
}