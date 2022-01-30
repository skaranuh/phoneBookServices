using System;

namespace PhoneBook.Api.Utilities
{
    public class Error
    {
        public ErrorCodes ErrrorCode { get; private set; }
        public string ErrorMessage { get; private set; }

        public Guid ErrorId { get; set; }
        public Error(ErrorCodes errorCode, string errorMessage)
        {
            ErrrorCode = errorCode;
            ErrorMessage = errorMessage;
            ErrorId = Guid.NewGuid();
        }
    }
}