using System;

namespace PhoneBook.Api.Utilities.Exceptions.Base
{
    public class BaseException : Exception
    {
        public ErrorCodes ErrorCode { get; protected set; }
        public BaseException(ErrorCodes errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}