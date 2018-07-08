using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;
using System.Net;
using static CMSToken.Web.Handlers.GlobalExceptionHandler;
using System.Linq;

namespace CMSToken.Web.Handlers
{
    public abstract class MessageHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var corrId = string.Format("{0}{1}", DateTime.Now.Ticks, Thread.CurrentThread.ManagedThreadId);
            var requestInfo = string.Format("{0} {1}", request.Method, request.RequestUri);

            IEnumerable<KeyValuePair<string, string>> queryStrings = request.GetQueryNameValuePairs();

            var requestMessage = await request.Content.ReadAsByteArrayAsync();

            var response = await base.SendAsync(request, cancellationToken);

            byte[] responseMessage;
            bool hasError = false;
            string responseMessageString = string.Empty;
            /*Code for checking that response has some error or not*/
            if (response.IsSuccessStatusCode)
            {
                responseMessage = await response.Content.ReadAsByteArrayAsync();
                responseMessageString = Encoding.UTF8.GetString(responseMessage);
            }
            else
            {
                responseMessage = Encoding.UTF8.GetBytes(response.ReasonPhrase);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var loanId = "TOKEN";

                    var match = queryStrings.FirstOrDefault(kv => string.Compare(kv.Key, "id", true) == 0);
                    if (!string.IsNullOrEmpty(match.Value))
                        loanId = match.Value;

                    response = request.CreateResponse(HttpStatusCode.Unauthorized, new ErrorResponse { Message = response.ReasonPhrase, Count = 0, Status = 1, LoanId = loanId });

                }

                if (response.Content != null)
                {
                    responseMessageString = Encoding.UTF8.GetString(responseMessage) + " " + Encoding.UTF8.GetString(await response.Content.ReadAsByteArrayAsync());
                }
                else responseMessageString = Encoding.UTF8.GetString(responseMessage);

                hasError = true;
            }
            await OutgoingMessageAsync(corrId, requestInfo, responseMessageString, queryStrings, hasError);

            return response;
        }


        protected abstract Task OutgoingMessageAsync(string correlationId, string requestInfo, string message, IEnumerable<KeyValuePair<string, string>> queryStrings, bool hasError);
    }
}