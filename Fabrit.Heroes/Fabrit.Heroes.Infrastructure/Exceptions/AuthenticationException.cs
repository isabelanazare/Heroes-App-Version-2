using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Fabrit.Heroes.Infrastructure.Common.Exceptions
{
    public class AuthenticationException : ApiExceptionBase
    {
        public AuthenticationException() : base(HttpStatusCode.BadRequest, Constants.AUTHENTICATION_EXCEPTION)
        {
        }

        public AuthenticationException(string message) : base(HttpStatusCode.BadRequest, message, Constants.AUTHENTICATION_EXCEPTION)
        {
        }

        public AuthenticationException(string message, Exception innerException) : base(HttpStatusCode.BadRequest, message, innerException, Constants.AUTHENTICATION_EXCEPTION)
        {
        }
    }
}
