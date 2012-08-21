using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Core.Enums;
using MobileSite.BaseLib.MemberContent;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MobileSite.BaseLib
{
    public class ApiFactory
    {
        public static void FormatResult(ref string result, string apiName,MemberInfo memberInfo, HttpContext context)
        {
            try
            {
                var resultJson = JObject.Parse(result);
                var dataStr = context.Request["_data"] ?? "";
                var dataJson = string.IsNullOrEmpty(dataStr) ? null : JObject.Parse(dataStr);
                var resultStatus = resultJson.Value<long>("status");

                #region Member.Login
                if (apiName.Equals("Member.Login", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (resultStatus == (long)MResultStatus.Success && dataJson != null && dataJson.Count > 0)
                    {
                        var user_id = resultJson.Value<int>("info");
                        var uid = dataJson.Value<string>("uid");
                        var token = resultJson.Value<string>("data");
                        WebUtility.SetMemberSession(user_id, uid, token);
                    }
                }
                #endregion

                #region Member.logout
                else if (apiName.Equals("Member.logout", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (resultStatus == (long)MResultStatus.Success)
                    {
                        WebUtility.RemoveMemberSession();
                        WebUtility.RefreshGuid();
                    }
                }
                #endregion

                #region Member.Register
                else if (apiName.Equals("Member.Register", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (resultStatus == (long)MResultStatus.Success && dataJson != null && dataJson.Count > 0)
                    {
                        var user_id = resultJson.Value<int>("info");
                        var uid = dataJson.Value<string>("uid");
                        var token = resultJson.Value<string>("data");
                        WebUtility.SetMemberSession(user_id,uid, token);
                    }
                }
                #endregion

                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
