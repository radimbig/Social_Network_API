using System;
using System.Threading.Tasks;
using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Social_Network_API.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
namespace Social_Network_API.Common.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next) =>
            _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = StatusCodes.Status500InternalServerError;
            string result = string.Empty;
            if(exception is ICustomException customException)
            {
                code = customException.StatusCode;
                result = JsonSerializer.Serialize(new { error=customException.View });
            }   
            if(exception is ValidationException validationException)
            {
                code = StatusCodes.Status400BadRequest;
                result = JsonSerializer.Serialize(validationException.Errors);
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = code;
            
            if (result == string.Empty)
            {
                result = JsonSerializer.Serialize(new { error = exception.Message });
            }

            return context.Response.WriteAsync(result);
        }
    }
}
