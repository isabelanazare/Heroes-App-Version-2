using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Fabrit.Heroes.Infrastructure.Common.Exceptions
{
    public class InvalidParameterException : ApiExceptionBase
    {
        public InvalidParameterException() : base(HttpStatusCode.BadRequest, Constants.INVALID_PARAMETER_EXCEPTION)
        {
        }

        public InvalidParameterException(string parameterName, Exception innerException) : base(HttpStatusCode.BadRequest, $"Parameter name: {parameterName}", innerException, Constants.INVALID_PARAMETER_EXCEPTION)
        {
        }

        public InvalidParameterException(string parameterName)
            : base(HttpStatusCode.BadRequest, $"Parameter name: {parameterName}", Constants.INVALID_PARAMETER_EXCEPTION)
        {
        }
    }
}
