using System.Web.Mvc;
using System.Web.Routing;

namespace SC.UI.Web.MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("Default", "{culture}/{controller}/{action}/{id}", new { culture = "en-US", controller = "Home", action = "Index", id = UrlParameter.Optional }
 );
        }
    }
}
