using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Core.DataType;
using Wcf.Entity.Cms;
using System.ServiceModel.Web;

namespace Wcf.ServiceLibrary.Cms
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    public interface ICmsService
    {
        /// <summary>
        /// 获取栏位数据列表
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="token"></param>
        /// <param name="guid"></param>
        /// <param name="user_id"></param>
        /// <param name="uid"></param>
        /// <param name="columncode"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = CmsUri.GETCOLUMNDATALIST)]
        MResultList<ColumnData> GetColumnDataList(string sid, string token, string guid, string user_id, string uid, string columncode, string page, string size);

        /// <summary>
        /// 获取公告列表
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="token"></param>
        /// <param name="guid"></param>
        /// <param name="user_id"></param>
        /// <param name="uid"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = CmsUri.GETNOTICELIST)]
        MResultList<ItemNotice> GetNoticeList(string sid, string token, string guid, string user_id, string uid, string page, string size);

        /// <summary>
        /// 获取公告详细
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="token"></param>
        /// <param name="guid"></param>
        /// <param name="user_id"></param>
        /// <param name="uid"></param>
        /// <param name="noticeid"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = CmsUri.GETNOTICEINFO)]
        MResult<ItemNotice> GetNoticeInfo(string sid, string token, string guid, string user_id, string uid, string noticeid);

    }
}
