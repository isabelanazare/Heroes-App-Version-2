using Fabrit.Heroes.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;
namespace Fabrit.Heroes.Web.Infrastructure.Middleware
{
    public class ExceptionMiddleware
    {
        public async Task Invoke(HttpContext context)
        {
            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature != null && contextFeature.Error != null)
            {
                context.Response.ContentType = "application/json";
                var problemDetails = GetProblemDetails(context, contextFeature);
                await context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails));
            }
        }

        private static ProblemDetails GetProblemDetails(HttpContext context, IExceptionHandlerFeature exception)
        {
            if (!Extensions.IsSystemException(exception.Error))
            {
                if (exception.Error is ApiExceptionBase exceptionBase)
                {
                    return new HeroesProblemDetails(exceptionBase);
                }
            }
            
            return new ProblemDetails
            {
                Status = context.Response.StatusCode,
                Title = "Exception",
                Detail = exception.Error.Message
            };
        }
    }
}
