using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Core.DataType;
using System.ServiceModel.Web;
using Wcf.Entity.Manage.Cms;

namespace Wcf.ServiceLibrary.Manage
{
    /// <summary>
    /// 系统管理服务
    /// </summary>
    [ServiceContract]
    public interface IManageService
    {
        /// <summary>
        /// 刷新授权数据
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="token"></param>
        /// <param name="guid"></param>
        /// <param name="user_id"></param>
        /// <param name="uid"></param>
        /// <param name="privatekey"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = ManageUri.REFRESHAUTHDATA)]
        MResult RefreshAuthData(string sid, string token, string guid, string user_id, string uid, string privatekey);

        /// <summary>
        /// 刷新缓存组版本
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="token"></param>
        /// <param name="guid"></param>
        /// <param name="user_id"></param>
        /// <param name="uid"></param>
        /// <param name="cachegroup"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = ManageUri.REFRESHCACHEGROUPVERSION)]
        MResult RefreshCacheGroupVersion(string sid, string token, string guid, string user_id, string uid,
                                         string cachegroup);

    }
}
