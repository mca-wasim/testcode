using log4net;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using System.Xml.Serialization;

namespace CMSToken.Web.Handlers
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        private static ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private const string source = "CMS Web API";
        public GlobalExceptionHandler()
        {
        }

        public async override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            IEnumerable<KeyValuePair<string, string>> queryStrings = context.Request.GetQueryNameValuePairs();
            var loanId = "TOKEN";

            var match = queryStrings.FirstOrDefault(kv => string.Compare(kv.Key, "id", true) == 0);
            if (!string.IsNullOrEmpty(match.Value))
                loanId = match.Value;

            var exception = context.Exception;

            log4net.Config.XmlConfigurator.Configure();
            string errorMessage = "An unexpected error occured";

            GlobalContext.Properties["Source"] = source;

            logger.Error(errorMessage, context.Exception);

            //Modify response
            HttpResponseMessage response = null;
            response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, new ErrorResponse { Message = "There is some problem in server", Count = 0, Status = 1, LoanId = loanId });
            response.Headers.Add("X-Error", errorMessage);
            context.Result = new ResponseMessageResult(response);
        }

        [XmlType(AnonymousType = true)]
        [XmlRoot("response", Namespace = "", IsNullable = false)]
        public partial class ErrorResponse
        {
            /// <remarks/>
            [XmlElement("message")]
            public string Message { get; set; }

            /// <remarks/>
            [XmlAttribute("count")]
            public int Count { get; set; }

            /// <remarks/>
            [XmlAttribute("status")]
            public int Status { get; set; }

            /// <remarks/>
            [XmlAttribute("loan-id")]
            public string LoanId { get; set; }
        }
    }
}