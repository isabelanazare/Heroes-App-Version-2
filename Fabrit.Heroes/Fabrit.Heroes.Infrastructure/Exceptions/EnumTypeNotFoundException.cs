using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Fabrit.Heroes.Infrastructure.Common.Exceptions
{
    public class EnumTypeNotFoundException : ApiExceptionBase
    {
        public EnumTypeNotFoundException() : base(HttpStatusCode.NotFound, Constants.ENUM_TYPE_NOT_FOUND_EXCEPTION)
        {
        }

        public EnumTypeNotFoundException(string message) : base(HttpStatusCode.NotFound, message, Constants.ENUM_TYPE_NOT_FOUND_EXCEPTION)
        {
        }

        public EnumTypeNotFoundException(string message, Exception innerException) : base(HttpStatusCode.NotFound, message, innerException, Constants.ENUM_TYPE_NOT_FOUND_EXCEPTION)
        {
        }
    }
}
