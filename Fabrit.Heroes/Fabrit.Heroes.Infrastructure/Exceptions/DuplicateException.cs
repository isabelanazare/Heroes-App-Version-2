using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Fabrit.Heroes.Infrastructure.Common.Exceptions
{
    public class DuplicateException : ApiExceptionBase
    {
        public DuplicateException() : base(HttpStatusCode.Conflict, Constants.DUPLICATE_EXCEPTION)
        {
        }

        public DuplicateException(string message) : base(HttpStatusCode.Conflict, message, Constants.DUPLICATE_EXCEPTION)
        {
        }
                    
        public DuplicateException(string message, Exception innerException) : base(HttpStatusCode.Conflict, message, innerException, Constants.DUPLICATE_EXCEPTION)
        {
        }
    }
}
