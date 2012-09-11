using System;
using System.Web;
using System.Net;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Collections.Specialized;
using MobileSite.BaseLib;

namespace MobileSite
{
    /// <summary>
    /// AjaxProxy 的摘要说明
    /// </summary>
    public partial class RequestProxy : BaseApi, IHttpHandler
    {
        /// <summary>
        /// 数据转换
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                var api = context.Request["_api"] ?? "";
                var url = ApiUrlDict.ContainsKey(api) ? ApiUrlDict[api] : context.Request["_url"];
                var method = context.Request["_type"] ?? "GET";
                var data = context.Request["_data"] ?? "";
                var contentType = context.Request["_contentType"] ?? "";
                var callback = context.Request["callback"];
                var result = string.Empty;

                if (!string.IsNullOrEmpty(url))
                {
                    FillGetParams(ref url, context);
                    var webRequest = (HttpWebRequest)WebRequest.Create(new Uri(url));
                    webRequest.Timeout = 1000 * 60;
                    webRequest.Method = method.ToUpper();

                    if (method.Equals("POST", StringComparison.InvariantCultureIgnoreCase))
                    {
                        FillPostParams(ref data, context, webRequest);
                        var _params = Encoding.ASCII.GetBytes(data);
                        webRequest.ContentLength = _params.Length;
                        webRequest.ContentType = !string.IsNullOrEmpty(contentType) ? contentType : "application/x-www-form-urlencoded";
                        using (var reqStream = webRequest.GetRequestStream())
                        {
                            reqStream.Write(_params, 0, _params.Length);
                        }
                    }
                    else if (method.Equals("GET", StringComparison.InvariantCultureIgnoreCase))
                    {

                    }

                    using (var webResponse = (HttpWebResponse)webRequest.GetResponse())
                    {
                        if (webResponse.StatusCode == HttpStatusCode.OK)
                        {
                            var streamResponse = webResponse.GetResponseStream();
                            if (streamResponse != null)
                            {
                                var streamRead = new StreamReader(streamResponse, System.Text.Encoding.UTF8);
                                result = streamRead.ReadToEnd();
                            }
                        }
                        else
                            result = "调用错误！";
                    }
                }

                context.Response.Write(string.IsNullOrEmpty(callback)
                                           ? string.Format("{0}", result)
                                           : string.Format("{0}({1});", callback, result));
            }
            catch (Exception ex)
            {
                context.Response.Write("程序错误！" + ex);
            }
        }

        /// <summary>
        /// 填充 Get 参数
        /// </summary>
        /// <param name="url"></param>
        /// <param name="context"> </param>
        /// <returns></returns>
        void FillGetParams(ref string url, HttpContext context)
        {
            var keys = context.Request.QueryString.AllKeys;
            var keysLen = keys.Length;
            var appendParam = context.Request["_params"] ?? "";
            var _params = new List<string>();
            for (var i = 0; i < keysLen; i++)
            {
                var key = keys[i];
                if (!key.StartsWith("_") && !key.Equals("callback", StringComparison.InvariantCultureIgnoreCase))
                    _params.Add(string.Format("{0}={1}", key, context.Request.QueryString[key]));
            }

            if (!string.IsNullOrEmpty(appendParam) || _params.Count > 0)
                url = string.Format("{0}/{1}?{2}",
                    url.TrimEnd(new char[] { '?', '/', ' ' }),
                    appendParam.TrimStart('/'),
                    string.Join("&", _params.ToArray()));

            else
                url = string.Format("{0}/{1}",
                url.TrimEnd(new char[] { '?', '/', ' ' }),
                MConfigUtility.Get("SystemType").Trim('/'));
        }

        /// <summary>
        /// 填充 Post 参数
        /// </summary>
        /// <param name="data"></param>
        /// <param name="context"> </param>
        /// <param name="webRequest"> </param>
        /// <returns></returns>
        void FillPostParams(ref string data, HttpContext context, HttpWebRequest webRequest)
        {
            var keys = context.Request.Form.AllKeys;
            var keysLen = keys.Length;
            var _params = new List<string>();
            if ((context.Request["_contentType"] ?? "").Equals("application/json", StringComparison.InvariantCulture))
            {

            }
            else
            {
                for (var i = 0; i < keysLen; i++)
                {
                    var key = keys[i];
                    if (!key.StartsWith("_") && !key.Equals("callback", StringComparison.InvariantCultureIgnoreCase))
                        _params.Add(string.Format("{0}={1}", key, context.Request.Form[key]));
                }
                data = string.Join("&", _params.ToArray());
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}