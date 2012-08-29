using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MobileSite.BaseLib;

namespace MobileSite
{
    public partial class index : BasePage
    {
        /// <summary>
        /// 设置 页面ID
        /// </summary>
        /// <param name="pageId"></param>
        public void SetMasterInfo(string pageId, string pageTitle)
        {
            var master = Master as Mobile;
            if (master != null)
            {
                master.PageId = pageId;
                master.PageTitle = pageTitle;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            SetMasterInfo("Index_Page", "首页");
        }
    }
}