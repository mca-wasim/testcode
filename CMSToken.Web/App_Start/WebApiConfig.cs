using System.Web.Http;

namespace CMSToken.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            // config.Routes.MapHttpRoute(
            //    name: "loanmod_default",
            //    routeTemplate: "loanmod/v1/{controller}/{action}/{id}",
            //    defaults: new { action = "Index", id = RouteParameter.Optional }
            //);

        }
    }
}
