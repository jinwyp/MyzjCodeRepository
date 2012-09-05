using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.Caching;
using System.Xml.Linq;

namespace MobileSite.BaseLib
{
    /// <summary>
    /// 功能：全站Url 地址管理类
    /// 说明：网站的url 通过读取配置文件的地址，并提供调用方法
    /// 创建日期：2012.6.6
    /// 创建人:张斌
    /// </summary>
    public class WebApis
    {
        private static readonly Dictionary<string, string> Apis;
        private static readonly Dictionary<string, string> Locations;

        /// <summary>
        /// 初始化读取配置数据
        /// </summary>
        static WebApis()
        {
            string xmlPath = string.Format("{0}/Configs/restful.config",
                                           HttpContext.Current.Server.MapPath("/").TrimEnd('/'));
            const string urlsCacheKey = "CONFIG_API";
            const string locationsCacheKey = "CONFIG_API_LOCATIONS";
            Apis = new Dictionary<string, string>();
            Locations = new Dictionary<string, string>();

            var urlsCacheData = HttpContext.Current.Cache.Get(urlsCacheKey);
            var locationsCacheData = HttpContext.Current.Cache.Get(urlsCacheKey);

            if (urlsCacheData != null && locationsCacheData != null)
            {
                Apis = urlsCacheData as Dictionary<string, string>;
                Locations = locationsCacheData as Dictionary<string, string>;
            }
            else
            {
                #region 解析配置文件

                try
                {
                    var el = XElement.Load(xmlPath);

                    #region 读取 location 节点

                    var locationsList = el.Descendants("location");
                    foreach (var locationItem in locationsList)
                    {
                        var name = locationItem.Attribute("name").Value;
                        var path = locationItem.Attribute("value").Value;
                        if (!string.IsNullOrEmpty(name))
                        {
                            Locations.Add(name, path);
                        }
                    }
                    HttpContext.Current.Cache.Add(locationsCacheKey, Locations, null, DateTime.MaxValue, new TimeSpan(0, 20, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
                    #endregion

                    #region 读取 url 节点

                    var urlsList = el.Descendants("api");
                    foreach (var urlsItem in urlsList)
                    {
                        string name = urlsItem.Attribute("name").Value;
                        string path = urlsItem.Attribute("value").Value;
                        string location = Locations[urlsItem.Attribute("location").Value ?? ""];
                        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(location))
                        {
                            Apis.Add(name, location + path);
                        }
                    }
                    HttpContext.Current.Cache.Add(urlsCacheKey, Apis, null, DateTime.MaxValue, new TimeSpan(0, 20, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
                    #endregion
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                #endregion
            }
        }

        /// <summary>
        /// 获取api 地址列表
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string,string> GetApiList()
        {
            var apiDict = new Dictionary<string, string>();
            foreach(var api in Apis)
            {
                apiDict.Add(api.Key, api.Value);
            }
            return apiDict;
        }

        /// <summary>
        /// 获取 Url 数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetUrlData(string name, params object[] args)
        {
            if (Apis.ContainsKey(name))
                if (args.Length > 0)
                    return string.Format(Apis[name].Replace('^', '&'), args);
                else
                    return Apis[name];
            else
                throw new Exception("节点未配置！ 请检查配置文件！");
        }

    }
}