using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using Core.DataType;
using Wcf.BLL.Manage;

namespace Wcf.ServiceLibrary.Manage
{
    /// <summary>
    /// 系统管理服务
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [JavascriptCallbackBehavior(UrlParameterName = CommonUri.JAVASCRIPT_CALLBACKNAME)]
    public class ManageService : BaseWcfService, IManageService
    {
        public MResult RefreshAuthData(string sid, string token, string guid, string user_id, string uid, string privatekey)
        {
            var result = new MResult();

            try
            {
                result = ManageBLL.RefreshAuthData(SystemType, Token, Guid, Uid, privatekey);

            }
            catch (Exception ex)
            {
                result.status = Core.Enums.MResultStatus.ExceptionError;
                result.msg = "处理数据出错！";
            }

            return result;
        }
    }
}
