using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using Wcf.BLL.BaseData;
using Wcf.Entity.BaseData;
using Core.DataType;
using Factory;
using Core.Enums;
using Core.DataTypeUtility;

namespace Wcf.ServiceLibrary.BaseData
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [JavascriptCallbackBehavior(UrlParameterName = CommonUri.JAVASCRIPT_CALLBACKNAME)]
    public class BaseDataService : BaseWcfService, IBaseDataService
    {
        public MResultList<ItemPay> GetPayList(string sid, string token, string guid, string user_id, string uid, string regionid)
        {
            var result = new MResultList<ItemPay>();
            try
            {
                var regionId = MCvHelper.To<int>(regionid);
                result = BaseDataBLL.GetPayList((int)SystemType, regionId);
            }
            catch (Exception ex)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "处理数据出错！";
            }
            return result;
        }

        public MResultList<ItemLogistics> GetLogisticsList(string sid, string token, string guid, string user_id, string uid, string regionid, string paygroupid)
        {
            var result = new MResultList<ItemLogistics>();
            try
            {
                var regionId = MCvHelper.To<int>(regionid);
                var paygroupId = MCvHelper.To<int>(paygroupid);

                result = BaseDataBLL.GetDeliverList((int)SystemType, regionId, paygroupId);
            }
            catch (Exception ex)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "处理数据出错！";
            }
            return result;
        }

        public MResultList<ItemRegion> GetRegionList(string sid, string token, string guid, string user_id, string uid, string parentid)
        {
            var result = new MResultList<ItemRegion>();
            try
            {
                var parentId = MCvHelper.To<int>(parentid);
                result = BaseDataBLL.GetRegionList(parentId);
            }
            catch (Exception)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "处理数据出错！";
            }
            return result;
        }

        public MResult<List<ItemRegion>[]> GetAllRegionList(string sid, string token, string guid, string user_id, string uid)
        {
            var result = new MResult<List<ItemRegion>[]>();
            try
            {
                result = BaseDataBLL.GetAllRegionList();
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
