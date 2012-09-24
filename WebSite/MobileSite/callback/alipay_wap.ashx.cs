﻿using System;
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
            var jobj = WebUtility.CallPaymentNotifyApi(context, 101);
            if (jobj.Value<int>("status") == 1)
            {
                var orderCode = jobj.Value<string>("info");
                context.Response.Redirect(WebUrls.orderdetail(orderCode));
            }
            else
            {
                context.Response.Write(jobj.Value<string>("msg"));
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