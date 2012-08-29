using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MobileSite.BaseLib;

namespace MobileSite
{
    public partial class Login : BasePage
    {
        /// <summary>
        /// 设置 页面ID
        /// </summary>
        /// <param name="pageId"></param>
        public void SetMasterPageId(string pageId, string pageTitle)
        {
            var master = Master as Mobile;
            if (master != null)
                master.PageId = pageId;
            master.PageTitle = pageTitle;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SetMasterPageId("Member_Login_Page", "登录页");
        }
    }
}