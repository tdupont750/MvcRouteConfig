using System;
using System.Configuration;

namespace MvcRouteConfig.Configuration
{
    [ConfigurationCollection(typeof(RouteConfigurationElement))]
    public class RouteConfigurationElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new RouteConfigurationElement();
        }

        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((RouteConfigurationElement)element).Name;
        }

        public RouteConfigurationElement this[int index]
        {
            get { return (RouteConfigurationElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);

                BaseAdd(index, value);
            }
        }
    }
}