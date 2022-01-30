using System;
using PhoneBook.Api.Utilities.Exceptions.Base;

namespace PhoneBook.Api.Utilities.Exceptions
{
    public class EndUserException : BaseException
    {
        public EndUserException(ErrorCodes errorCode, string message) : base(errorCode, message)
        {
        }
    }
}