using System.Net;
using System.Net.Http;
using System.Web.Http;
using CMSToken.Repository;
using CMSToken.Web.Filters;
using CMSToken.Data;
using System.Threading.Tasks;
using CMSToken.Utility.Caching;
using System.Xml.Linq;
using System.Text;
using System.Linq;
using CMSToken.Utility.Builder;
using CMSToken.Utility.TokenFormator;
using CMSToken.Service.DataToken;
using CMSToken.Repository.DynamicQuery;
using static CMSToken.Web.Handlers.GlobalExceptionHandler;

namespace CMSToken.Web.Areas.loanmod.Controllers
{
    public class TokenDataController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly CMSTokenContext context;
        private readonly BitBDataWarehouseContext dbBitbContext;
        private readonly IDynamicQueryRepository dynamicQueryRepository;
        private readonly IDataService dataService;
        private readonly IClassBuilder classBuilder;
        private readonly ICacheService cacheService;
        private readonly ICMSTokenFormator cMSTokenFormator;
        private readonly IDataServiceCommon dataServiceCommon;
        public TokenDataController()
        {
            context = new CMSTokenContext();
            unitOfWork = new UnitOfWork(this.context);
            dbBitbContext = new BitBDataWarehouseContext();
            this.dynamicQueryRepository = new DynamicQueryRepository(dbBitbContext);
            this.classBuilder = new ClassBuilder();
            this.cacheService = new CacheService();
            cMSTokenFormator = new CMSTokenFormator();
            dataServiceCommon = new DataServiceCommon();
            this.dataService = new DataService(unitOfWork, dynamicQueryRepository, this.classBuilder, this.cacheService, cMSTokenFormator, dataServiceCommon);

        }

        [BasicAuthentication]
        [Authorize]
        // GET: api/TokenData/477537039
        public async Task<HttpResponseMessage> Get(string id = "")
        {
            if (!string.IsNullOrEmpty(id))
            {
                XDocument dataResponse = await this.dataService.GetDataResponseByLoanId(id);
                if (dataResponse != null && dataResponse.Elements().Count() > 0)
                {
                    var request = new HttpResponseMessage(HttpStatusCode.OK);
                    request.Content = new StringContent(dataResponse.ToString(), Encoding.UTF8, "application/xml");
                    return request;
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, new ErrorResponse { Message = "Loan not found.", Count = 0, Status = 1, LoanId = id });
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new ErrorResponse { Message = "Please provide Loan Id.", Count = 0, Status = 1, LoanId = id });
            }
        }

    }
}
