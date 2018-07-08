using System.Web.Http;
using System.Web.Mvc;

namespace CMSToken.Web.Areas.loanmod
{
    public class loanmodAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "loanmod";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.Routes.MapMvcAttributeRoutes();
            context.Routes.MapHttpRoute(
               name: "loanmod_default",
               routeTemplate:"api/loanmod/v1/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
           );
        }
    }
}