using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileSite
{
    public partial class MultiPage : System.Web.UI.MasterPage
    {
        public string PageId { get; set; }
        public string PageTitle { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}