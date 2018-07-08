using System.Threading.Tasks;
using CMSToken.Repository;
using CMSToken.Data;
using System;
using System.Collections.Generic;
using log4net;
using System.Reflection;
using CMSToken.Service.TokenAuditData;

namespace CMSToken.Web.Handlers
{
    public class MessageLoggingHandler : MessageHandler
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ITokenAuditService tokenAuditService;
        private CMSTokenContext context;
        private static ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public MessageLoggingHandler()
        {
            context = new CMSTokenContext();
            unitOfWork = new UnitOfWork(this.context);
            tokenAuditService = new TokenAuditService(this.unitOfWork);
        }

        protected override async Task OutgoingMessageAsync(string correlationId, string requestInfo, string message, IEnumerable<KeyValuePair<string, string>> queryStrings, bool hasError)
        {
            try
            {
                await this.tokenAuditService.Add(message, queryStrings, hasError);
            }
            catch (Exception ex)
            {
                logger.Error("An unexpected error occured while auditing response", ex);
            }
        }
    }
}