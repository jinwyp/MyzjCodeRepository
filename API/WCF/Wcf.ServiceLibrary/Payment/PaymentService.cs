using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;
using Core.DataType;
using Wcf.BLL.Payment;
using Core.DataTypeUtility;

namespace Wcf.ServiceLibrary.Payment
{
    /// <summary>
    /// 支付接口
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [JavascriptCallbackBehavior(UrlParameterName = CommonUri.JAVASCRIPT_CALLBACKNAME)]
    public class PaymentService : BaseWcfService, IPaymentService
    {
        public MResult<string> OrderPayment(string sid, string token, string guid, string user_id, string uid, string ocode, string payid)
        {
            var result = new MResult<string>();

            try
            {
                var payId = MCvHelper.To<int>(payid, 0);

                result = PaymentBLL.OrderPayment(SystemType, UserId, uid, ocode, payId);
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
