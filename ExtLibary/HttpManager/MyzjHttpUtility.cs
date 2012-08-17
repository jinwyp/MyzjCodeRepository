using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace HttpManager
{
    public static class MyzjHttpUtility
    {
        /// <summary>
        /// 检查 请求来源是否属于移动浏览器
        /// </summary>
        /// <param name="isSessionEnd">是否 Session End</param>
        public static void CheckRequestClient(bool isSessionEnd)
        {
            var httpContext = HttpContext.Current;
            if (!isSessionEnd)
            {
                var appSettings = ConfigurationManager.AppSettings;
                var webSiteUrl = appSettings.Get("WebSiteUrl");
                var mobileSiteUrl = appSettings.Get("MobileSiteUrl");

                if (string.IsNullOrEmpty(webSiteUrl)) throw new Exception("请配置 WebSiteUrl（b2c 网站） 节点");
                if (string.IsNullOrEmpty(mobileSiteUrl)) throw new Exception("请配置 MobileSiteUrl（手机 网站） 节点");

                var hostName = httpContext.Request.Url.Host.ToUpper();
                var referName = httpContext.Request["refer"];
                var referFlag = httpContext.Session["referFlag"];

                //判断 refer 为空 并且 session 没有保存过值，说明用户第一次访问 并且需要做浏览器类型的判断
                if (string.IsNullOrEmpty(referName) && referFlag == null)
                {
                    if (httpContext.Request.UserAgent != null)
                    {

                        #region 判断是否为移动浏览器

                        var isMobileClient = false;
                        var userAgent = httpContext.Request.UserAgent.ToUpper();
                        string[] userAgentKeyworkds = {
                                                          "ANDROID", "IPHONE", "IPOD", "IPAD", "WINDOWS PHONE",
                                                          "MQQBROWSER"
                                                      };

                        //排除 Windows 桌面系统
                        if (!userAgent.Contains("Windows NT") ||
                            (userAgent.Contains("Windows NT") && userAgent.Contains("compatible; MSIE 9.0;")))
                        {
                            //排除 苹果桌面系统
                            if (!userAgent.Contains("Windows NT") && !userAgent.Contains("Macintosh"))
                            {
                                if (userAgentKeyworkds.Any(userAgent.Contains))
                                {
                                    isMobileClient = true;
                                }
                            }
                        }

                        #endregion

                        #region 页面跳转

                        if (isMobileClient)
                        {
                            if (!mobileSiteUrl.ToUpper().Contains(hostName))
                            {
                                httpContext.Response.Redirect(mobileSiteUrl);
                            }
                        }
                        else
                        {
                            if (!webSiteUrl.ToUpper().Contains(hostName))
                            {
                                httpContext.Response.Redirect(webSiteUrl);
                            }
                        }

                        #endregion

                    }
                }
                else
                {
                    httpContext.Session.Add("referName", referName);
                }
            }else
            {
                httpContext.Session.RemoveAll();
            }
        }
    }

}
