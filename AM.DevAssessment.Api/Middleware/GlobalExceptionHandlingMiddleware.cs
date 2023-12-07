using AM.DevAssessment.Api.Models;
using System.Net;
using System.Text.Json;

namespace AM.DevAssessment.Api.Middleware;

public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    public GlobalExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var response = context.Response;
        ErrorResponseModel exModel = new ();

        switch (exception)
        {
            case ArgumentNullException:
            case ArgumentException:
                exModel.StatusCode = (int)HttpStatusCode.BadRequest;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                exModel.Message = exception.Message;
                break;
            default:
                exModel.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                exModel.Message = exception.Message;
                break;

        }
        var exResult = JsonSerializer.Serialize(exModel);
        await context.Response.WriteAsync(exResult);
    }
}