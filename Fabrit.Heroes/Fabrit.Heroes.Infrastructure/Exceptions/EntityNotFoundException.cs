using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Fabrit.Heroes.Infrastructure.Common.Exceptions
{
    public class EntityNotFoundException : ApiExceptionBase
    {
        public EntityNotFoundException() : base(HttpStatusCode.NotFound, Constants.ENTITITY_NOT_FOUND_EXCEPTION)
        {
        }

        public EntityNotFoundException(string message) : base(HttpStatusCode.NotFound, message, Constants.ENTITITY_NOT_FOUND_EXCEPTION)
        {
        }

        public EntityNotFoundException(string message, Exception innerException) : base(HttpStatusCode.NotFound, message, innerException, Constants.ENTITITY_NOT_FOUND_EXCEPTION)
        {
        }
    }
}
