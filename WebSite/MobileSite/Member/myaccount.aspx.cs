﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MobileSite.BaseLib.MemberContent;

namespace MobileSite
{
    public partial class myaccount : BaseMember
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckMemberLoginStatus();
        }
    }
}