using Fabrit.Heroes.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fabrit.Heroes.Web.Infrastructure
{
    public class HeroesProblemDetails : ProblemDetails
    {
        public HeroesProblemDetails(ApiExceptionBase apiExceptionBase)
        {
            Detail = apiExceptionBase.Message;
            Status = (int)apiExceptionBase.Code;
            Title = apiExceptionBase.Title;
        }
    }
}
