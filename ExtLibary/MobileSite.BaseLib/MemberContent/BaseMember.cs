using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MobileSite.BaseLib.MemberContent
{
    /// <summary>
    /// 会员中心调用，需要登录
    /// </summary>
    public class BaseMember : BasePage
    {
        public void Page_Init(object sender, EventArgs e)
        {
            CheckMemberLoginStatus();
        }

        /// <summary>
        /// 检查用户登录超时，跳转到登陆页面
        /// </summary>
        public void CheckMemberLoginStatus()
        {
            if (!WebUtility.VerifyMemberSession())
            {
                Response.Redirect(WebUrls.Login(HttpUtility.UrlEncode(Request.RawUrl, Encoding.UTF8)));
            }
        }
    }
}
