using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Fabrit.Heroes.Infrastructure.Common.Exceptions
{
    public class SendEmailException : ApiExceptionBase
    {
        public SendEmailException() : base(HttpStatusCode.Forbidden, Constants.SEND_EMAIL_EXCEPTION)
        {
        }

        public SendEmailException(string message) : base(HttpStatusCode.Forbidden, message, Constants.SEND_EMAIL_EXCEPTION)
        {
        }

        public SendEmailException(string message, Exception innerException) : base(HttpStatusCode.Forbidden, message, innerException, Constants.SEND_EMAIL_EXCEPTION)
        {
        }
    }
}
