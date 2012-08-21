using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MobileSite.BaseLib.MemberContent
{
    public class BaseMember : BasePage
    {
        /// <summary>
        /// 检查用户登录超时，跳转到登陆页面
        /// </summary>
        public void CheckMemberLoginStatus()
        {
            if (!WebUtility.VerifyMemberSession())
            {
                Response.Redirect(WebUrls.Login(HttpUtility.UrlEncode(Request.Url.AbsoluteUri, Encoding.UTF8)));
            }
        }
    }
}
