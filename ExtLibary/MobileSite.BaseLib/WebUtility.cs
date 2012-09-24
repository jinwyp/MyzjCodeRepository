using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Configuration;
using Core.DataType;
using Core.Enums;
using Newtonsoft.Json.Linq;

namespace MobileSite.BaseLib
{
    public class WebUtility : IRequiresSessionState
    {
        /// <summary>
        /// 调用 Api
        /// </summary>
        /// <param name="invokeParmeter"></param>
        /// <returns></returns>
        public static string InvokeRestApi(InvokeParmeter invokeParmeter)
        {
            var result = string.Empty;
            var flag = false;

            try
            {

                #region WebRequest

                var webRequest = (HttpWebRequest)WebRequest.Create(invokeParmeter.Uri);
                webRequest.Timeout = 1000 * 60;
                webRequest.Method = invokeParmeter.Method.ToString();

                if (invokeParmeter.Method == MMethodType.POST || invokeParmeter.Method == MMethodType.PUT)
                {
                    var _params = Encoding.UTF8.GetBytes(invokeParmeter.Data);

                    webRequest.ContentLength = _params.Length;
                    webRequest.ContentType = !string.IsNullOrEmpty(invokeParmeter.ContentType)
                                                 ? invokeParmeter.ContentType
                                                 : "application/x-www-form-urlencoded";
                    webRequest.Accept = !string.IsNullOrEmpty(invokeParmeter.ContentType)
                                                 ? invokeParmeter.ContentType
                                                 : "application/x-www-form-urlencoded";

                    using (var reqStream = webRequest.GetRequestStream())
                    {
                        reqStream.Write(_params, 0, _params.Length);
                    }
                    flag = true;
                }
                else if (invokeParmeter.Method == MMethodType.GET || invokeParmeter.Method == MMethodType.DELETE)
                {
                    flag = true;
                }
                if (flag)
                {
                    using (var webResponse = webRequest.GetResponse() as HttpWebResponse)
                    {
                        if (webResponse != null && webResponse.StatusCode == HttpStatusCode.OK)
                        {
                            var streamResponse = webResponse.GetResponseStream();
                            if (streamResponse != null)
                            {
                                var streamRead = new StreamReader(streamResponse, System.Text.Encoding.UTF8);
                                result = streamRead.ReadToEnd();
                            }
                        }
                        else
                        {
                            result = ApiResult("调用远程接口出错！");
                        }
                    }
                }
                else
                    result = ApiResult("Method 参数错误！");

                #endregion

                #region WebClient

                /*
                var webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                if (invokeParmeter.Method == MethodType.POST || invokeParmeter.Method == MethodType.PUT)
                {
                    var contentType = !string.IsNullOrEmpty(invokeParmeter.ContentType)
                                                 ? invokeParmeter.ContentType
                                                 : "application/x-www-form-urlencoded";
                    webClient.ResponseHeaders.Set(HttpRequestHeader.ContentType, contentType);
                    result = webClient.UploadString(invokeParmeter.Uri, invokeParmeter.Method.ToString(), invokeParmeter.Data);
                    flag = true;
                }
                else if (invokeParmeter.Method == MethodType.GET || invokeParmeter.Method == MethodType.DELETE)
                {
                    result = webClient.DownloadString(invokeParmeter.Uri);
                    flag = true;
                }
                else
                {
                    result = ApiResult("Method 参数错误！");
                }
                 */
                #endregion
            }
            catch (Exception ex)
            {
                result = ApiResult("初始化调用异常！" + ex);
            }
            return result;
        }

        /// <summary>
        /// 调用支付通知处理接口
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static JObject CallPaymentNotifyApi(HttpContext context, object paymentNotifyType)
        {
            var queryData = context.Request.QueryString;
            var formData = context.Request.Form;
            var postData = new StringBuilder();
            var getData = new StringBuilder();
            foreach (string formKey in formData)
                postData.AppendFormat("&{0}={1}", formKey, formData[formKey]);
            foreach (string getKey in queryData)
                getData.AppendFormat("&{0}={1}", getKey, queryData[getKey]);
            if (postData.Length > 0) postData.Remove(0, 1); else postData.Append("null");
            if (getData.Length > 0) getData.Remove(0, 1); else getData.Append("null");

            var queryUrl = BaseApi.ApiUrlDict.ContainsKey("Order.orderpayment_success")
                          ? BaseApi.ApiUrlDict["Order.orderpayment_success"]
                          : "";

            var jdata = new JObject(
                new JProperty("paymentnotifytype", paymentNotifyType),
                new JProperty("getdata", getData.ToString()),
                new JProperty("postdata", postData.ToString()));

            queryUrl += string.Format("/{0}/{1}/{2}/{3}/{4}",
                MConfigUtility.Get("SystemType"),
                "null",
                "null",
                "null",
                "null");
            var uri = new Uri(queryUrl);

            var resultJson = WebUtility.InvokeRestApi(new InvokeParmeter()
            {
                Uri = uri,
                Method = MMethodType.POST,
                ContentType = "application/json",
                Data = jdata.ToString()
            });
            return JObject.Parse(resultJson); ;
        }

        #region 设置返回信息
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string ApiResult(string msg)
        {
            return ApiResult(msg, MResultStatus.Undefined);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string ApiResult(MResultStatus status)
        {
            return ApiResult("调用错误！", status);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string ApiResult(string msg, MResultStatus status)
        {
            return ApiResult(msg, status, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="status"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ApiResult(string msg, MResultStatus status, string data)
        {
            return ToJson<MResult>(new MResult()
                                       {
                                           msg = msg,
                                           status = status,
                                           data = data
                                       });
        }
        #endregion

        #region 序列化
        /// <summary>
        /// 序列化 实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns>json 数据</returns>
        public static string ToJson<T>(T item)
        {
            var serializer = new DataContractJsonSerializer(item.GetType());
            try
            {
                using (var ms = new MemoryStream())
                {
                    serializer.WriteObject(ms, item);
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch
            {
            }
            return default(string);
        }
        /// <summary>
        /// 序列化 字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns>实体类</returns>
        public static T JsonTo<T>(string str) where T : class
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            try
            {
                using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(str)))
                {
                    return serializer.ReadObject(ms) as T;
                }
            }
            catch
            {

            }
            return default(T);
        }
        #endregion

        #region 会员Session

        /// <summary>
        /// 刷新Guid
        /// </summary>
        public static void RefreshGuid()
        {
            var session = HttpContext.Current.Session;
            if (session["s_guid"] != null)
                session.Remove("s_guid");
            session.Add("s_guid", Guid.NewGuid().ToString("N"));
        }

        /// <summary>
        /// 设置会员session
        /// </summary>
        /// <param name="user_id"> </param>
        /// <param name="uid"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool SetMemberSession(int user_id, string uid, string token)
        {
            var result = true;
            var httpContext = HttpContext.Current;
            try
            {
                httpContext.Session.Add("s_user_id", user_id);
                httpContext.Session.Add("s_uid", uid);
                httpContext.Session.Add("s_token", token);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 移除会员session
        /// </summary>
        /// <returns></returns>
        public static bool RemoveMemberSession()
        {
            var result = true;
            var httpContext = HttpContext.Current;
            try
            {
                httpContext.Session.Remove("s_user_id");
                httpContext.Session.Remove("s_uid");
                httpContext.Session.Remove("s_token");
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 验证会员session
        /// </summary>
        /// <returns></returns>
        public static bool VerifyMemberSession()
        {
            var result = false;
            var httpContext = HttpContext.Current;
            try
            {
                var sUser_id = httpContext.Session["s_user_id"] == null ? 0 : int.Parse(httpContext.Session["s_user_id"].ToString());
                var sUid = httpContext.Session["s_uid"];
                var sToken = httpContext.Session["s_token"];
                //var sGuid = httpContext.Session["s_guid"];
                if (sUid != null && sToken != null && sUser_id > 0)
                    result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        #endregion

    }

    /// <summary>
    /// 调用api参数
    /// </summary>
    public class InvokeParmeter
    {
        public Uri Uri { get; set; }

        public MMethodType Method { get; set; }

        public string Data { get; set; }

        public string ContentType { get; set; }

        public string Accept { get; set; }

        public string Callback { get; set; }
    }
}