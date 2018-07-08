using CMSToken.Service.Common;
using CMSToken.Utility.Caching;
using CMSToken.Web.Filters;
using System.Web.Http;

namespace CMSToken.Web.Areas.loanmod.Controllers
{
    public class CommonController : ApiController
    {
        private readonly ICommonCode commonCode;
        private readonly ICacheService cacheService;
        public CommonController()
        {
            cacheService = new CacheService();
            commonCode = new CommonCode(cacheService);
        }

       

        [CacheAuthentication]
        [Authorize]
        // POST: api/Common
        public IHttpActionResult Post()
        {
            commonCode.ClearCache();
            return Ok("Refreshed");
        }      
    }
}