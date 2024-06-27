using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EmployeeManagment.Middlewares
{
    public class CustomMiddleware1
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware1(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            /*
            try
            {
                Console.WriteLine("TRY BLOCK");
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine("CATCH BLOCK");
                Console.WriteLine(e);
                return;
            }
            finally
            {
                Console.WriteLine("Finally BLOCK");
            }*/
            
            try
            {
                Console.WriteLine("Custom Middleware 1");
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = System.Text.Json.JsonSerializer.Serialize(new { error = exception.Message });
            return context.Response.WriteAsync(result);
        }
    }
}