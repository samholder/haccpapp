using System.Web.Http;
using Microsoft.Owin.Security.OAuth;

namespace hacapp.web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Configure WebApi to use only bearer token authentication
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/id",
                defaults: new {id = RouteParameter.Optional});
        }
    }
}