using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Collections.Specialized;

namespace Core.ConfigUtility
{
    public class MConfigurationSectionHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            var baseHandler = new NameValueSectionHandler();
            var configs = (NameValueCollection)baseHandler.Create(parent, configContext, section);
            return configs;
        }
    }
}
