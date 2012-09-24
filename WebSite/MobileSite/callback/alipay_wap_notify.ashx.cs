using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MobileSite.BaseLib;

namespace MobileSite.callback
{
    /// <summary>
    /// alipay_wap_notify 的摘要说明
    /// </summary>
    public class alipay_wap_notify : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var jobj = WebUtility.CallPaymentNotifyApi(context, 102);
            if (jobj.Value<int>("status") == 1)
            {
                context.Response.Write("success");
            }
            else
            {
                context.Response.Write("fail");
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