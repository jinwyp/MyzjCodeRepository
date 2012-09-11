using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MobileSite.BaseLib;
using Newtonsoft.Json.Linq;
using System.Text;

namespace MobileSite.callback
{
    /// <summary>
    /// alipay_wap 的摘要说明
    /// </summary>
    public class alipay_wap : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
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
            queryUrl += string.Format("/{0}/{1}/{2}/{3}/{4}/{5}/{6}",
                MConfigUtility.Get("SystemType"),
                "null",
                "null",
                "null",
                "null",
                HttpUtility.UrlEncodeUnicode(getData.ToString()),
                HttpUtility.UrlEncodeUnicode(postData.ToString()));
            var uri = new Uri(queryUrl);

            var resultJson = WebUtility.InvokeRestApi(new InvokeParmeter()
            {
                Uri = uri,
                Method = Core.Enums.MMethodType.GET
            });
            var jobj = JObject.Parse(resultJson);
            if (jobj.Value<int>("status") == 1)
            {
                var orderCode = jobj.Value<string>("info");
                context.Response.Redirect(WebUrls.orderdetail(orderCode));
            }
            else
            {
                context.Response.Write("参数错误！");
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