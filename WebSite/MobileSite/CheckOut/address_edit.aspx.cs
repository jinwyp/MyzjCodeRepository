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
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckMemberLoginStatus();
        }
    }
}