using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.ErrorsLogs;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env){

            _next = next;
            _logger = logger;
            _env = env;

        }

        public async Task InvokeAsync(HttpContext _context){
            try{

                await _next(_context);
            }catch(Exception ex){
                    _logger.LogError(ex,ex.Message);
                    _context.Response.ContentType="application/json";
                    _context.Response.StatusCode=(int) HttpStatusCode.InternalServerError;
                    var Response=_env.IsDevelopment() ? new ErrorExecption(_context.Response.StatusCode,ex.Message,ex.StackTrace?.ToString())
                    : new ErrorExecption(_context.Response.StatusCode,"Internal server error");

                    var option= new JsonSerializerOptions{
                        PropertyNamingPolicy=JsonNamingPolicy.CamelCase
                    };
                    var json=JsonSerializer.Serialize(Response,option);

                    await _context.Response.WriteAsync(json);

            }
        }
    }
}