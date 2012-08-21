
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobileSite.BaseLib
{
    /// <summary>
    /// 
    /// </summary>
    public class BasePage : System.Web.UI.Page
    {
        public void Page_Load()
        {
            WebUtility.RefreshGuid();
        }

    }
}
