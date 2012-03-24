using System.Configuration;

namespace MvcRouteConfig.Configuration
{
    public class RouteConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("url", IsRequired = true)]
        public string Url
        {
            get { return (string)this["url"]; }
            set { this["url"] = value; }
        }

        [ConfigurationProperty("routeHandlerType", IsRequired = false)]
        public string RouteHandlerType
        {
            get { return (string)this["routeHandlerType"]; }
            set { this["routeHandlerType"] = value; }
        }

        [ConfigurationProperty("defaults", IsRequired = false)]
        public RouteChildConfigurationElement Defaults
        {
            get { return (RouteChildConfigurationElement)this["defaults"]; }
            set { this["defaults"] = value; }
        }

        [ConfigurationProperty("dataTokens", IsRequired = false)]
        public RouteChildConfigurationElement DataTokens
        {
            get { return (RouteChildConfigurationElement)this["dataTokens"]; }
            set { this["dataTokens"] = value; }
        }

        [ConfigurationProperty("constraints", IsDefaultCollection = false, IsRequired = false)]
        public ConstraintConfigurationElementCollection Constraints
        {
            get { return base["constraints"] as ConstraintConfigurationElementCollection; }
        }
    }
}