using System.Web.Routing;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(MvcRouteConfig.Web.RouteConfigurationActivator), "Activate")]
namespace MvcRouteConfig.Web
{
    public static class RouteConfigurationActivator
    {
        public static void Activate()
        {
            RouteTable.Routes.RegisterConfigurationBundles();
        }
    }
}