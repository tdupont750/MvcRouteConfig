using System.Configuration;

namespace MvcRouteConfig.Configuration
{
    public class ConstraintConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get { return (string)this["type"]; }
            set { this["type"] = value; }
        }

        [ConfigurationProperty("params", IsRequired = false)]
        public RouteChildConfigurationElement Params
        {
            get { return (RouteChildConfigurationElement)this["params"]; }
            set { this["params"] = value; }
        }
    }
}