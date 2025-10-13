using System;
using System.Dynamic;
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
        catch ( Exception ex)
        {
            logger.LogError(ex.Message, "{message}", ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var response = env.IsDevelopment() ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace)
            : new ApiException(context.Response.StatusCode, ex.Message, "internal server error");
            var JsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var json = JsonSerializer.Serialize(response, JsonOptions);
           await  context.Response.WriteAsync(json);  
             
        }
    }
}
