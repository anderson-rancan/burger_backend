using System;
using System.Runtime.Serialization;

namespace BurgerBackend.Identity.Interface.Exceptions
{
    public class BugerBackendIdentityException : Exception
    {
        public BugerBackendIdentityException()
        {
        }

        public BugerBackendIdentityException(string message) : base(message)
        {
        }

        public BugerBackendIdentityException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BugerBackendIdentityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
