using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace jwt.MiddleWare;

public class GlobalErrorHandling:IMiddleware
{
     
    private readonly ILogger logger;

    public GlobalErrorHandling(ILogger<GlobalErrorHandling> logger )
    { 
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {

            logger.LogError(e, e.Message);
            ProblemDetails problem = new()
            {
                Title = e.Message,
                Status = (int)HttpStatusCode.InternalServerError,
                Detail = "the server error occurred"
            };

            string json = JsonSerializer.Serialize(problem);
            await context.Response.WriteAsync(json);
            context.Response.ContentType = "application/json";
        }
    }

     
}
