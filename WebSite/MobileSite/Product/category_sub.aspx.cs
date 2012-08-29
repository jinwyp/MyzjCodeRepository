using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MobileSite.BaseLib;

namespace MobileSite.Product
{
    public partial class category_sub : BasePage
    {
        /// <summary>
        /// 设置 页面ID
        /// </summary>
        /// <param name="pageId"></param>
        public void SetMasterPageId(string pageId)
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
            SetMasterPageId("Product_SubCategory_Page");
        }
    }
}