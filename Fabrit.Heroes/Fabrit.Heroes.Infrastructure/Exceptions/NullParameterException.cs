using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Fabrit.Heroes.Infrastructure.Common.Exceptions
{
    public class NullParameterException : ApiExceptionBase
    {
        public NullParameterException() : base(HttpStatusCode.BadRequest, Constants.NULL_PARAMETER_EXCEPTION)
        {
        }

        public NullParameterException(string parameterName) : base(HttpStatusCode.BadRequest, $"Parameter name: {parameterName}", Constants.NULL_PARAMETER_EXCEPTION)
        {
        }

        public NullParameterException(string parameterName, Exception innerException) : base(HttpStatusCode.BadRequest, $"Parameter name: {parameterName}", innerException, Constants.NULL_PARAMETER_EXCEPTION)
        {
        }
    }
}
