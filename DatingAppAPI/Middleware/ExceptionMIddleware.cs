using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using DatingAppAPI.Errors;

namespace DatingAppAPI.Middleware
{
  public class ExceptionMIddleware
  {
    public RequestDelegate _next { get; }
    private readonly ILogger<ExceptionMIddleware> _logger;
    public IHostEnvironment _env { get; }
    public ExceptionMIddleware(RequestDelegate next, ILogger<ExceptionMIddleware> logger,
        IHostEnvironment env)
    {
      _env = env;
      _logger = logger;
      _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      try
      {
        await _next(context);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, ex.Message);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = _env.IsDevelopment()
            ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
            : new ApiException(context.Response.StatusCode, ex.Message, "Internal Server Error");

        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var json = JsonSerializer.Serialize(response, options);
        await context.Response.WriteAsync(json);

      }
    }
  }
}