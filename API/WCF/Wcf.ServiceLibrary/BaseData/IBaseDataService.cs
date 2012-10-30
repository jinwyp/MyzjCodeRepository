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
        /// <param name="uid"> </param>
        /// <param name="regionid"> </param>
        /// <param name="paygroupid"> </param>
        /// <param name="guid"> </param>
        /// <param name="user_id"> </param>
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
        /// <param name="parentid"> </param>
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

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="token"></param>
        /// <param name="guid"></param>
        /// <param name="user_id"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = BaseDataUri.GETCAPTCHA)]
        MResult<string> GetCaptcha(string sid, string token, string guid, string user_id, string uid);

        /// <summary>
        /// 检验验证码
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="token"></param>
        /// <param name="guid"></param>
        /// <param name="user_id"></param>
        /// <param name="uid"></param>
        /// <param name="verifycode">待验证验证码字符串</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = BaseDataUri.VERIFYCAPTCHA)]
        MResult<bool> VerifyCaptcha(string sid, string token, string guid, string user_id, string uid,string verifysig, string verifycode);

        /// <summary>
        /// 获取验证码流
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="token"></param>
        /// <param name="guid"></param>
        /// <param name="user_id"></param>
        /// <param name="uid"></param>
        [OperationContract]
        [WebGet(UriTemplate = BaseDataUri.GETCAPTCHASTREAM)]
        void GetCaptchaStream(string sid, string token, string guid, string user_id, string uid,string verifysig);

    }
}
