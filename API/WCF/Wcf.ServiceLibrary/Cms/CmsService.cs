using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using Core.DataType;
using Core.Enums;
using Wcf.Entity.Cms;
using Core.Caching;
using Wcf.BLL.Cms;
using Core.DataTypeUtility;

namespace Wcf.ServiceLibrary.Cms
{
    /// <summary>
    /// 
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [JavascriptCallbackBehavior(UrlParameterName = CommonUri.JAVASCRIPT_CALLBACKNAME)]
    public class CmsService : BaseWcfService, ICmsService
    {

        public MResultList<ColumnData> GetColumnDataList(string sid, string token, string guid, string user_id, string uid, string columncode, string page, string size)
        {
            var result = new MResultList<ColumnData>();

            try
            {
                var pageIndex = MCvHelper.To(page, 1);
                var pageSize = MCvHelper.To(size, 1);
                result = MCacheManager.UseCached<MResultList<ColumnData>>(
                    string.Format("GetColumnDataList_{0}_{1}_{2}_{3}", sid, columncode, pageIndex, pageSize),
                     Core.Enums.MCaching.CacheGroup.Cms,
                     () => CmsBLL.GetColumnDataList(SystemType, user_id, uid, columncode, pageIndex, pageSize)
                    );
            }
            catch (Exception ex)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "处理数据出错！";
            }

            return result;
        }

        public MResultList<ItemNotice> GetNoticeList(string sid, string token, string guid, string user_id, string uid, string page, string size)
        {
            var result = new MResultList<ItemNotice>();
            try
            {
                var pageIndex = MCvHelper.To(page, 1);
                var pageSize = MCvHelper.To(size, 1);
                result = MCacheManager.UseCached<MResultList<ItemNotice>>(
                    string.Format("GetNoticeList_{0}_{1}_{2}", sid, pageIndex, pageSize),
                     MCaching.CacheGroup.Cms,
                     () => CmsBLL.GetNoticeList(SystemType, user_id, uid, pageIndex, pageSize)
                    );
            }
            catch (Exception)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "处理数据出错！";
            }
            return result;
        }

        public MResult<ItemNotice> GetNoticeInfo(string sid, string token, string guid, string user_id, string uid, string noticeid)
        {
            var result = new MResult<ItemNotice>();
            try
            {
                var noticeId = MCvHelper.To(noticeid, 0);
                result = MCacheManager.UseCached<MResult<ItemNotice>>(
                    string.Format("GetNoticeInfo_{0}_{1}_{2}_{3}", sid, user_id, uid, noticeId),
                     MCaching.CacheGroup.Cms,
                     () => CmsBLL.GetNoticeInfo(SystemType, noticeId)
                    );
            }
            catch (Exception)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "处理数据出错！";
            }
            return result;
        }
    }
}
