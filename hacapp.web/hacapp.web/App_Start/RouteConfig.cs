using System.Web.Mvc;
using System.Web.Routing;

namespace hacapp.web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("MembershipUpdates", "{controller}/{teamId}/{action}/{idOfUserToUpdate}/{newStatus}");

            routes.MapRoute("ActionsWithTeamIds", "{controller}/{action}/{teamId}",
                new {controller = "Home", action = "Index", teamId = UrlParameter.Optional});

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional});
        }
    }
}