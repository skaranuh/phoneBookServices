using PhoneBook.Api.Utilities.Exceptions.Base;

namespace PhoneBook.Api.Utilities.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(ErrorCodes.NotFound, message)
        {
        }
    }
}