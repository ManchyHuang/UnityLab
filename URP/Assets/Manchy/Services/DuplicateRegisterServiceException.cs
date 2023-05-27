using System;

namespace Manchy
{
    public class DuplicateRegisterServiceException : Exception
    {
        public DuplicateRegisterServiceException() : base()
        {
        }
        public DuplicateRegisterServiceException(string message) : base(message)
        {
        }
        public DuplicateRegisterServiceException(Exception innerException) : base("", innerException)
        { 
        }
        public DuplicateRegisterServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}