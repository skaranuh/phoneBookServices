using System;
using System.Net;

namespace PhoneBook.Api.Utilities
{
    public interface IExceptionHelper
    {
        string GetErrorMessage(Exception error);
        ErrorCodes GetErrorEnum(Exception error);
        HttpStatusCode GetErrorStatusCode(Exception error);
    }
}