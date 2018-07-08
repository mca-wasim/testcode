using System;
using System.Web.Http;
using System.Web.Mvc;
using CMSToken.Web.Areas.HelpPage.ModelDescriptions;
using CMSToken.Web.Areas.HelpPage.Models;

namespace CMSToken.Web.Areas.HelpPage.Controllers
{
    /// <summary>
    /// The controller that will handle requests for the help page.
    /// </summary>
    public class HelpController : Controller
    {
        private const string ErrorViewName = "Error";

        public HelpController()
            : this(GlobalConfiguration.Configuration)
        {
        }

        public HelpController(HttpConfiguration config)
        {
            Configuration = config;
        }

        public HttpConfiguration Configuration { get; private set; }

        public ActionResult Index()
        {
            return View("");
        }

        public ActionResult Api(string apiId)
        {
           
            return View(ErrorViewName);
        }

        
    }
}