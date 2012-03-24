using System.Configuration;

namespace MvcRouteConfig.Configuration
{
    public class RouteConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("routes", IsDefaultCollection = false)]
        public RouteConfigurationElementCollection Routes
        {
            get { return base["routes"] as RouteConfigurationElementCollection; }
        }
    }
}