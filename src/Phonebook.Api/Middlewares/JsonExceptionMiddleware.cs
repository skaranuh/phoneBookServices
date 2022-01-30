using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PhoneBook.Api.Utilities;
using PhoneBook.Api.Utilities.Exceptions;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PhoneBook.Api
{
    public class JsonExceptionMiddleware
    {
        private readonly ILogger<JsonExceptionMiddleware> _logger;
        private readonly IExceptionHelper _exceptionHelper;

        public JsonExceptionMiddleware(ILogger<JsonExceptionMiddleware> logger, IExceptionHelper exceptionHelper)
        {
            _logger = logger;
            _exceptionHelper = exceptionHelper;
        }
        public async Task Invoke(HttpContext context)
        {
            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature != null && contextFeature.Error != null)
            {
                var errorCode = _exceptionHelper.GetErrorEnum(contextFeature.Error);
                context.Response.StatusCode = (int)_exceptionHelper.GetErrorStatusCode(contextFeature.Error);
                context.Response.ContentType = "application/json";
                var errorMessage = _exceptionHelper.GetErrorMessage(contextFeature.Error);

                await context.Response.WriteAsync(JsonSerializer.Serialize(new Error(errorCode, errorMessage)
                , new JsonSerializerOptions
                {
                    Converters ={new JsonStringEnumConverter()
    }
                }));
            }
        }
    }
}
