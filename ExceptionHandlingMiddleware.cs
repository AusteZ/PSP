using System;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using PSP.Models;

namespace PSP;
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (UserFriendlyException ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, UserFriendlyException exception)
    {
        context.Response.StatusCode = exception.StatusCode;
        context.Response.ContentType = "text/plain";
        return context.Response.WriteAsync(exception.Message);
    }
}
