using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Reflection;
using System.IO;
using System.Text;
using System.ServiceModel;
using System;
using MobileSite.BaseLib;
using System.Web.SessionState;
using MemberInfo = MobileSite.BaseLib.MemberContent.MemberInfo;

namespace MobileSite
{
    /// <summary>
    /// WebApi 的摘要说明
    /// </summary>
    public class WebApi : BaseApi, IHttpHandler
    {
        private MemberInfo _memberInfo;
        public MemberInfo MemberInfo
        {
            get { return _memberInfo; }
            set { _memberInfo = value; }
        }

        public void ProcessRequest(HttpContext context)
        {
            string result;

            var session = context.Session;
            var userId = int.Parse((session["s_user_id"] != null ? session["s_user_id"].ToString() : "0"));
            var uid = session["s_uid"] != null ? session["s_uid"].ToString() : "";
            var token = session["s_token"] != null ? session["s_token"].ToString() : "";
            var guid = session["s_guid"] != null ? session["s_guid"].ToString() : "";
            var systemType = MConfigUtility.Get("SystemType");
            var systemkey = MConfigUtility.Get("SystemKey");
            var requestTime = DateTime.Now;

            _memberInfo = new MemberInfo
            {
                UserId = userId,
                Uid = uid,
                ToKen = token,
                Guid = guid,
                SystemType = systemType
            };

            var callback = context.Request["callback"];
            try
            {
                var apiName = context.Request["_api"] ?? "";
                var apiHost = ApiUrlDict.ContainsKey(apiName) ? ApiUrlDict[apiName] : "";
                var url = context.Request["_url"] ?? "";
                if (string.IsNullOrEmpty(apiHost)) { result = WebUtility.ApiResult("api 不能为空！"); }
                else
                {
                    var data = context.Request["_data"] ?? "";

                    var requestUrl = string.Format("{0}/{1}/{2}/{3}/{4}/{5}{6}",
                                                   apiHost,
                                                   MemberInfo.SystemType,
                                                   string.IsNullOrEmpty(MemberInfo.ToKen) ? "null" : MemberInfo.ToKen,
                                                   string.IsNullOrEmpty(MemberInfo.Guid) ? "null" : MemberInfo.Guid,
                                                   MemberInfo.UserId,
                                                   string.IsNullOrEmpty(MemberInfo.Uid) ? "null" : MemberInfo.Uid,
                                                   string.IsNullOrEmpty(url) ? "" : "/" + url.Trim('/'));

                    var uri = new Uri(requestUrl
                        + "?time="
                        + requestTime
                        + "&md5="
                        + System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(string.Format("{0}{1}{2}{3}", requestUrl, requestTime, data, systemkey).ToUpper(), "MD5"));

                    var method = (MethodType)Enum.Parse(typeof(MethodType), context.Request["_type"] ?? "GET");

                    var contentType = context.Request["_contentType"] ?? "";
                    var accept = context.Request["_accept"] ?? "";
                    result = WebUtility.InvokeRestApi(new InvokeParmeter()
                    {
                        Uri = uri,
                        Method = method,
                        Data = data,
                        Callback = callback,
                        ContentType = contentType,
                        Accept = accept
                    });
                    ApiFactory.FormatResult(ref result, apiName,_memberInfo, context);
                }
            }
            catch (Exception ex)
            {
                result = WebUtility.ApiResult(ex.ToString());
            }
            if (!string.IsNullOrEmpty(callback))
                context.Response.Write(string.Format("{0}({1});", callback, result));
            else
                context.Response.Write(result);
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