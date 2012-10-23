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
            const string locationsCacheKey = "CONFIG_URL_LOCATIONS";
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

        const string VersionKey = "RESOURCEVERSION";
        static string versionId = string.Empty;
        /// <summary>
        /// 获取资源 版本
        /// </summary>
        public static string GetResourceVersion
        {
            get
            {
                if (string.IsNullOrEmpty(versionId))
                    versionId = DateTime.Now.ToString("yyyyMMdd-HHmmss");
                #region 使用缓存
                //string versionId;
                //try
                //{
                //    var cacheVal = HttpContext.Current.Cache.Get(VersionKey) as string;
                //    if (cacheVal == null || string.IsNullOrEmpty(cacheVal))
                //    {
                //        versionId = DateTime.Now.ToString("yyyyMMdd-HHmmss");
                //        HttpContext.Current.Cache.Add(VersionKey, versionId, null,
                //            DateTime.Now.AddMinutes(30),
                //            TimeSpan.Zero,
                //             CacheItemPriority.Default,
                //             null);
                //    }
                //    else
                //    {
                //        versionId = cacheVal;
                //    }
                //}
                //catch
                //{
                //    versionId = "0";
                //} 
                #endregion
                return versionId;
            }
        }

        /// <summary>
        /// 刷新 资源 版本
        /// </summary>
        public static string RefreshResourceVwersion()
        {
            //HttpContext.Current.Cache.Remove(VersionKey);
            versionId = "";
            return GetResourceVersion;
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
        public static string onlinepayment(object ocode, object paygroup)
        {
            return GetUrlData("onlinepayment", ocode, paygroup);
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
        public static string noticedetail(object notice_id)
        {
            return GetUrlData("noticedetail", notice_id);
        }

        /// <summary>
        /// 忘记密码
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static string forgetpassword()
        {
            return GetUrlData("forgetpassword");
        }

        /// <summary>
        /// 商品专题
        /// </summary>
        /// <returns></returns>
        public static string GoodsTopic(object id, object code)
        {
            return GetUrlData("goodstopic", id, code);
        }

        #endregion

    }
}