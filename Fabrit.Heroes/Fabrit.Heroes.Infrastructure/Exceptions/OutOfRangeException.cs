using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Fabrit.Heroes.Infrastructure.Common.Exceptions
{
    public class OutOfRangeException : ApiExceptionBase
    {
        public OutOfRangeException() : base(HttpStatusCode.BadRequest, Constants.OUT_OF_RANGE_EXCEPTION_TITLE)
        {
        }

        public OutOfRangeException(string parameterName) : base(HttpStatusCode.BadRequest, $"Parameter: {parameterName}", Constants.OUT_OF_RANGE_EXCEPTION_TITLE) 
        { 
        }

        public OutOfRangeException(string parameterName, Exception innerException) : base(HttpStatusCode.BadRequest, $"Parameter: {parameterName}", innerException, Constants.OUT_OF_RANGE_EXCEPTION_TITLE)
        {
        }
    }
}
