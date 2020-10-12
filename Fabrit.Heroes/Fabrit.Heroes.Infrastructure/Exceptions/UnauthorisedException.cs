using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Fabrit.Heroes.Infrastructure.Common.Exceptions
{
    public class UnauthorisedException : ApiExceptionBase
    {
        public UnauthorisedException() : base(HttpStatusCode.Unauthorized, Constants.UNAUTHORIZED_EXCEPTION)
        {
        }

        public UnauthorisedException( string message) : base(HttpStatusCode.Unauthorized, message, Constants.UNAUTHORIZED_EXCEPTION)
        {
        }

        public UnauthorisedException( string message, Exception innerException) : base(HttpStatusCode.Unauthorized, message, innerException, Constants.UNAUTHORIZED_EXCEPTION)
        {
        }
    }
}
