using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Fabrit.Heroes.Infrastructure.Common.Exceptions
{
    public class AccountAlreadyConfirmed : ApiExceptionBase
    {
        public AccountAlreadyConfirmed() : base(HttpStatusCode.BadRequest, Constants.ACCOUNT_ALREADY_CONFIRMED_EXCEPTION)
        {
        }

        public AccountAlreadyConfirmed(string message) : base(HttpStatusCode.BadRequest, message, Constants.ACCOUNT_ALREADY_CONFIRMED_EXCEPTION)
        {
        }

        public AccountAlreadyConfirmed(string message, Exception innerException) : base(HttpStatusCode.BadRequest, message, innerException, Constants.ACCOUNT_ALREADY_CONFIRMED_EXCEPTION)
        {
        }
    }
}
