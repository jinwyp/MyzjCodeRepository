using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MobileSite.BaseLib;

namespace MobileSite.Manage
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Label1.Text = string.Format("当前版本：{0}", WebUrls.GetResourceVersion);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var versionId = WebUrls.RefreshResourceVwersion();
            this.Label1.Text = string.Format("当前版本：{0}", versionId);
        }
    }
}