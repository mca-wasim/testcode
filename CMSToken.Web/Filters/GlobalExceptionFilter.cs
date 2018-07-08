using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace CMSToken.Web.Filters
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exception = actionExecutedContext.Exception;
            var exceptionType = exception.GetType();
            string errorMessage = string.Empty;
            //const string genericErrorMessage = "An error has occurred";
            //HttpResponseMessage response = null;
     

            //if(exceptionType == typeof(CalculationFailedException))
            //{
            //    response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            //    {
            //        Content = new StringContent(exception.Message)
            //    };

            //    response.Headers.Add("X-Error", genericErrorMessage);

            //}


            //base.OnException(actionExecutedContext);

        }




    }
}