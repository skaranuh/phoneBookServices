using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Authentication;
using PhoneBook.Api.Utilities.Exceptions;
using PhoneBook.Api.Utilities.Exceptions.Base;

namespace PhoneBook.Api.Utilities{
    public class ExceptionHelper : IExceptionHelper
    {
        public ErrorCodes GetErrorEnum(Exception error)
        {
            if (error.GetType() == typeof(EndUserException) || error.GetType() == typeof(NotFoundException))
            {
                return ((BaseException)error).ErrorCode;
            }
            if (error.GetType() == typeof(UnauthorizedAccessException))
            {
                return ErrorCodes.Access;
            }

            return ErrorCodes.Unknown;
        }

        public string GetErrorMessage(Exception error)
        {
            if (error.GetType() == typeof(EndUserException) || error.GetType() == typeof(NotFoundException) )
            {
                return ((BaseException)error).Message;
            }
            else if (error.GetType() == typeof(UnauthorizedAccessException))
            {
                return error.Message;
            }
            var message = "unknown error";

            return message;
        }
        public HttpStatusCode GetErrorStatusCode(Exception error)
        {
            return error switch
            {
                UnauthorizedAccessException _=> HttpStatusCode.Unauthorized,
                NotFoundException _=>  HttpStatusCode.NotFound,
                ValidationException _ => HttpStatusCode.BadRequest,
                FormatException _ => HttpStatusCode.BadRequest,
                EndUserException=> HttpStatusCode.BadRequest,
                AuthenticationException _ => HttpStatusCode.Forbidden,
                NotImplementedException _ => HttpStatusCode.NotImplemented,
                _ => HttpStatusCode.InternalServerError,
            };
        }
    }
}
