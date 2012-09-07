using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Core.DataType;
using Wcf.Entity.BaseData;
using System.ServiceModel.Web;

namespace Wcf.ServiceLibrary.BaseData
{
    [ServiceContract]
    public interface IBaseDataService
    {
        /// <summary>
        /// 获取支付方式
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="token"></param>
        /// <param name="regionid"> </param>
        /// <returns></returns>
        [WebGet(UriTemplate = BaseDataUri.GETPAYMENTLIST)]
        [OperationContract]
        MResultList<ItemPay> GetPayMentList(string sid, string token, string guid, string user_id, string uid, string regionid);

        /// <summary>
        /// 获取支付列表
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="token"></param>
        /// <param name="guid"></param>
        /// <param name="user_id"></param>
        /// <param name="uid"></param>
        /// <param name="paygroupid"></param>
        /// <returns></returns>
        [WebGet(UriTemplate = BaseDataUri.GETPAYLIST)]
        [OperationContract]
        MResultList<ItemPay> GetPayList(string sid, string token, string guid, string user_id, string uid,
                                                 string paygroupid);

        /// <summary>
        /// 获取配送方式
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="token"></param>
        /// <param name="regionid"> </param>
        /// <param name="paygroupid"> </param>
        /// <returns></returns>
        [WebGet(UriTemplate = BaseDataUri.GETLOGISTICSLIST)]
        [OperationContract]
        MResultList<ItemLogistics> GetLogisticsList(string sid, string token, string guid, string user_id, string uid, string regionid, string paygroupid);

        /// <summary>
        /// 获取区域数据
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="token"></param>
        /// <param name="guid"></param>
        /// <param name="user_id"></param>
        /// <param name="uid"></param>
        /// <param name="regionid"></param>
        /// <returns></returns>
        [WebGet(UriTemplate = BaseDataUri.GETREGIONLIST)]
        [OperationContract]
        MResultList<ItemRegion> GetRegionList(string sid, string token, string guid, string user_id, string uid,
                                                     string parentid);

        /// <summary>
        /// 获取全部区域数据
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="token"></param>
        /// <param name="guid"></param>
        /// <param name="user_id"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        [WebGet(UriTemplate = BaseDataUri.GETALLREGIONLIST)]
        [OperationContract]
        MResult<List<ItemRegion>[]> GetAllRegionList(string sid, string token, string guid, string user_id, string uid);

    }
}
