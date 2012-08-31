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
    public class WebUrls
    {
        private static readonly Dictionary<string, string> Urls;
        private static readonly Dictionary<string, string> Locations;

        /// <summary>
        /// 初始化读取配置数据
        /// </summary>
        static WebUrls()
        {
            string xmlPath = string.Format("{0}/Configs/SiteUrls.config",
                                           HttpContext.Current.Server.MapPath("/").TrimEnd('/'));
            const string urlsCacheKey = "CONFIG_URLS";
            const string locationsCacheKey = "CONFIG_LOCATIONS";
            Urls = new Dictionary<string, string>();
            Locations = new Dictionary<string, string>();

            var urlsCacheData = HttpContext.Current.Cache.Get(urlsCacheKey);
            var locationsCacheData = HttpContext.Current.Cache.Get(urlsCacheKey);

            if (urlsCacheData != null && locationsCacheData != null)
            {
                Urls = urlsCacheData as Dictionary<string, string>;
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
                        var path = locationItem.Attribute("path").Value;
                        if (!string.IsNullOrEmpty(name))
                        {
                            Locations.Add(name, path);
                        }
                    }
                    HttpContext.Current.Cache.Add(locationsCacheKey, Locations, null, DateTime.MaxValue, new TimeSpan(0, 20, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
                    #endregion

                    #region 读取 url 节点

                    var urlsList = el.Descendants("url");
                    foreach (var urlsItem in urlsList)
                    {
                        string name = urlsItem.Attribute("name").Value;
                        string path = urlsItem.Attribute("path").Value;
                        string location = Locations[urlsItem.Attribute("location").Value ?? ""];
                        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(location))
                        {
                            Urls.Add(name, location + path);
                        }
                    }
                    HttpContext.Current.Cache.Add(urlsCacheKey, Urls, null, DateTime.MaxValue, new TimeSpan(0, 20, 0), System.Web.Caching.CacheItemPriority.AboveNormal, null);
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
        /// 获取 Url 数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string GetUrlData(string name, params object[] args)
        {
            if (Urls.ContainsKey(name))
                if (args.Length > 0)
                    return string.Format(Urls[name].Replace('^', '&'), args);
                else
                    return Urls[name];
            else
                throw new Exception("节点未配置！ 请检查配置文件！");
        }

        /// <summary>
        /// 获取资源 缓存
        /// </summary>
        public static string GetResourceVersion
        {
            get
            {
                const string versionKey = "RESOURCEVERSION";
                var versionId = string.Empty;
                var cacheVal = HttpContext.Current.Cache.Get(versionKey);
                if (cacheVal != null && string.IsNullOrEmpty(cacheVal as string))
                {
                    versionId = DateTime.Now.ToString("yyyyMMddHHmmss");
                    HttpContext.Current.Cache.Add(versionKey, versionId, null,
                        DateTime.Now.AddMinutes(30),
                        new TimeSpan(0, 30, 0),
                         CacheItemPriority.Default,
                         null);
                }
                return versionId;
            }
        }

        #region 公共地址

        /// <summary>
        /// 获取网站根路径
        /// </summary>
        /// <returns></returns>
        public static string WebRoot()
        {
            return GetUrlData("WebRoot");
        }

        /// <summary>
        /// 获取js 根路径
        /// </summary>
        /// <returns></returns>
        public static string JsRoot()
        {
            return GetUrlData("JsRoot");
        }

        /// <summary>
        /// 获取图片根路径
        /// </summary>
        /// <returns></returns>
        public static string PicRoot()
        {
            return GetUrlData("PicRoot");
        }

        /// <summary>
        /// 获取主题根路径
        /// </summary>
        /// <returns></returns>
        public static string ThemesRoot()
        {
            return GetUrlData("ThemesRoot");
        }

        /// <summary>
        /// 获取 Wcf Host
        /// </summary>
        /// <returns></returns>
        public static string WcfHost()
        {
            return GetUrlData("WcfHost");
        }

        /// <summary>
        /// B2C 网站url
        /// </summary>
        /// <returns></returns>
        public static string B2CSite()
        {
            return GetUrlData("B2CSite");
        }

        #endregion

        #region 获取 页面地址（Url）

        /// <summary>
        /// 首页地址
        /// </summary>
        /// <returns></returns>
        public static string index()
        {
            return GetUrlData("index");
        }

        /// <summary>
        /// 用户中心
        /// </summary>
        /// <returns></returns>
        public static string myaccount()
        {
            return GetUrlData("myaccount");
        }

        /// <summary>
        /// 登陆页
        /// </summary>
        /// <returns></returns>
        public static string Login(params string[] returnUrl)
        {
            if (returnUrl.Length > 0)
                return GetUrlData("Login", returnUrl[0]);
            else
                return GetUrlData("Login", "");
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        public static string registration()
        {
            return GetUrlData("registration");
        }

        /// <summary>
        /// 商品详细页
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static string productdetailinfo(object gid)
        {
            return GetUrlData("productdetailinfo", gid);
        }

        /// <summary>
        /// 商品列表页
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static string productlist()
        {
            return GetUrlData("productlist");
        }

        /// <summary>
        /// 购物车
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static string shoppingcart()
        {
            return GetUrlData("shoppingcart");
        }

        /// <summary>
        /// 提交订单
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static string orderconfirm()
        {
            return GetUrlData("orderconfirm");
        }

        /// <summary>
        /// 选择支付方式
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static string paymentlist()
        {
            return GetUrlData("paymentlist");
        }

        /// <summary>
        /// 选择配送方式
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static string deliverylist()
        {
            return GetUrlData("deliverylist");
        }

        /// <summary>
        /// 填写发票信息
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static string invoice()
        {
            return GetUrlData("invoice");
        }

        /// <summary>
        /// 选择收货地址
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static string addresslist()
        {
            return GetUrlData("addresslist");
        }

        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static string makeorder()
        {
            return GetUrlData("makeorder");
        }

        /// <summary>
        /// 在线支付
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static string onlinepayment()
        {
            return GetUrlData("onlinepayment");
        }

        /// <summary>
        /// 订单中心
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static string orderlist()
        {
            return GetUrlData("orderlist");
        }

        /// <summary>
        /// 订单中心
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static string twomonths()
        {
            return GetUrlData("twomonths");
        }

        /// <summary>
        /// 订单详情
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static string orderdetail(object ocode)
        {
            return GetUrlData("orderdetail", ocode);
        }

        /// <summary>
        /// 添加新的收货地址
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static string address_add()
        {
            return GetUrlData("address_add");
        }

        /// <summary>
        /// 修改收货地址
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static string address_edit(object address_id)
        {
            return GetUrlData("address_edit", address_id);
        }


        /// <summary>
        /// 商品分类
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static string category()
        {
            return GetUrlData("category");
        }

        /// <summary>
        /// 商品二级分类
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static string category_sub()
        {
            return GetUrlData("category_sub");
        }

        /// <summary>
        /// 公告列表
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static string noticelist()
        {
            return GetUrlData("noticelist");
        }

        /// <summary>
        /// 公告详情
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static string noticedetail()
        {
            return GetUrlData("noticedetail");
        }

        #endregion

    }
}