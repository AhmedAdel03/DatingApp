using System;
using System.Net;
using System.Text.Json;
using Api.Error;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Api.Middleware;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)  
{
    

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex,"{message}",ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var Response = env.IsDevelopment() ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace.ToString())
              : new ApiException(context.Response.StatusCode, ex.Message, "internal server error");
            var option = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var json = JsonSerializer.Serialize(Response, option);
            await context.Response.WriteAsync(json);
             
        }
    }
}
