using System.Collections.Generic;
using System.Configuration;

namespace MvcRouteConfig.Configuration
{
    [ConfigurationCollection(typeof(ConstraintConfigurationElement))]
    public class ConstraintConfigurationElementCollection : ConfigurationElementCollection
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

        protected override ConfigurationElement CreateNewElement()
        {
            return new ConstraintConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ConstraintConfigurationElement)element).Name;
        }

        public ConstraintConfigurationElement this[int index]
        {
            get { return (ConstraintConfigurationElement) BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);

                BaseAdd(index, value);
            }
        }
    }
}