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
using Core.LogUtility;
using Core.Enums;
using Core.DataTypeUtility;

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
                result.status = MResultStatus.ExceptionError;
                result.msg = "调用业务逻辑异常！";
                throw new Exception(result.msg, ex);
            }

            return result;
        }

        public MResult RefreshCacheGroupVersion(string sid, string token, string guid, string user_id, string uid,
                                         string cachegroup)
        {
            var result = new MResult();
            try
            {
                throw new Exception("test");
                var cacheGroup = MCvHelper.To<MCaching.CacheGroup>(cachegroup);
                result = ManageBLL.RefreshCacheGroupVersion(SystemType, user_id, uid, cacheGroup);
            }
            catch (Exception ex)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "调用业务逻辑异常！";
                throw new Exception(result.msg, ex);
            }
            return result;
        }

    }
}
