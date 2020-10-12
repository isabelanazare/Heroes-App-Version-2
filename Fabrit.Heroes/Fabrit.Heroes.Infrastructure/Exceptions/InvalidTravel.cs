using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Fabrit.Heroes.Infrastructure.Common.Exceptions
{
    public class InvalidTravel : ApiExceptionBase
    {
        public InvalidTravel() : base(HttpStatusCode.BadRequest, Constants.INVALID_FILE_EXCEPTION)
        {
        }

        public InvalidTravel(string message) : base(HttpStatusCode.BadRequest, message, Constants.INVALID_FILE_EXCEPTION)
        {
        }

        public InvalidTravel(string message, Exception innerException) : base(HttpStatusCode.BadRequest, message, innerException, Constants.INVALID_FILE_EXCEPTION)
        {
        }
    }
}
