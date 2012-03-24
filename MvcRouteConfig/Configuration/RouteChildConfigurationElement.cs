using System.Collections.Generic;
using System.Configuration;

namespace MvcRouteConfig.Configuration
{
    public class RouteChildConfigurationElement : ConfigurationElement
    {
        private readonly Dictionary<string, string> _attributes = new Dictionary<string, string>();

        public Dictionary<string, string> Attributes
        {
            get { return _attributes; }
        }

        protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
        {
            if (_attributes.ContainsKey(name))
                return false;

            _attributes.Add(name, value);
            return true;
        }
    }
}
