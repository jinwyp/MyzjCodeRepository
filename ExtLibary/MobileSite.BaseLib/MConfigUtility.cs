using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Collections.Specialized;

namespace MobileSite.BaseLib
{
    public static class MConfigUtility
    {
        private static NameValueCollection _appSettings;

        static MConfigUtility()
        {
            if (_appSettings == null)
                _appSettings = System.Configuration.ConfigurationManager.AppSettings;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="name"></param>
        public static string Get(string name)
        {
            return _appSettings.AllKeys.Contains(name) ? _appSettings.Get(name) : "";
        }
    }
}
