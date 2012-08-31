using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MobileSite.BaseLib.MemberContent;

namespace MobileSite.CheckOut
{
    public partial class address_edit : BaseMember
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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            SetMasterPageId("CheckOut_Edit_Address_Page", "修改收货地址信息页");
        }
    }
}