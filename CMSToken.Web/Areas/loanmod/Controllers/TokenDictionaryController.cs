using CMSToken.Data;
using CMSToken.Model;
using CMSToken.Repository;
using CMSToken.Repository.DynamicQuery;
using CMSToken.Service.DictionaryToken;
using CMSToken.Utility.Caching;
using CMSToken.Utility.Common;
using CMSToken.Utility.Validator;
using CMSToken.Web.Filters;
using System.Web.Http;

namespace CMSToken.Web.Areas.loanmod.Controllers
{
    public class TokenDictionaryController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly CMSTokenContext context;
        private readonly IDictionaryService dictionaryService;
        private readonly ICacheService cacheService;
        private readonly IDynamicQueryRepository dynamicQueryRepository;
        private readonly BitBDataWarehouseContext bitbContext;
        private readonly ITokenDataTypeValidator tokenDataTypeValidator;
        private readonly ICommon commonCode;

        public TokenDictionaryController()
        {
            context = new CMSTokenContext();
            unitOfWork = new UnitOfWork(this.context);
            cacheService = new CacheService();
            bitbContext = new BitBDataWarehouseContext();
            dynamicQueryRepository = new DynamicQueryRepository(bitbContext);
            tokenDataTypeValidator = new TokenDataTypeValidator();
            commonCode = new Common();
            this.dictionaryService = new DictionaryService(unitOfWork, cacheService, dynamicQueryRepository, tokenDataTypeValidator, commonCode);
        }


        [BasicAuthentication]
        [Authorize]
        // GET: api/TokenDictionary
        public DictionaryResponse Get()
        {
            return this.dictionaryService.Get();
        }
        
        // POST: api/TokenDictionary
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/TokenDictionary/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/TokenDictionary/5
        public void Delete(int id)
        {
        }
    }
}
