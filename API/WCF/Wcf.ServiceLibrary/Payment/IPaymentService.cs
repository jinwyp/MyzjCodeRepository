using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Core.DataType;
using System.ServiceModel.Web;

namespace Wcf.ServiceLibrary.Payment
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    public interface IPaymentService
    {
        /// <summary>
        /// 订单支付
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="token"></param>
        /// <param name="guid"></param>
        /// <param name="user_id"></param>
        /// <param name="uid"></param>
        /// <param name="ocode"></param>
        /// <param name="payid"></param>
        /// <returns></returns>
        [WebInvoke(Method = "GET", UriTemplate = PaymentUri.ORDERPAYMENT)]
        [OperationContract]
        MResult<string> OrderPayment(string sid, string token, string guid, string user_id, string uid, string ocode, string payid);
    }
}
