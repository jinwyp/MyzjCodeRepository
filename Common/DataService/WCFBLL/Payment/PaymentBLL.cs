using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Core.DataType;
using Wcf.Entity.Enum;
using Factory;
using EF.Model.DataContext;
using Core.Payment;

namespace Wcf.BLL.Payment
{
    /// <summary>
    /// 支付 功能
    /// </summary>
    public class PaymentBLL
    {
        /// <summary>
        /// 订单支付
        /// </summary>
        /// <param name="sType"></param>
        /// <param name="userId"></param>
        /// <param name="uid"></param>
        /// <param name="oCode"></param>
        /// <param name="payId"></param>
        /// <returns></returns>
        public static MResult<string> OrderPayment(SystemType sType, int userId, string uid, string oCode, int payId)
        {
            var result = new MResult<string>();

            try
            {
                if (userId > 0 && !string.IsNullOrEmpty(oCode) && payId > 0)
                {
                    var orderDal = DALFactory.Order();
                    var baseDataDal = DALFactory.BaseData();
                    var memberDal = DALFactory.Member();

                    var memberInfo = memberDal.GetMemberInfo(userId);
                    #region 验证用户id
                    if (memberInfo == null || memberInfo.membNo <= 0)
                    {
                        result.status = Core.Enums.MResultStatus.LogicError;
                        result.msg = "用户不存在！";
                        return result;
                    }
                    #endregion

                    var orderInfo = orderDal.GetOrderInfo(oCode);
                    //订单编码正确
                    if (orderInfo != null && orderInfo.intOrderNO > 0)
                    {
                        #region 验证订单创建用户
                        if (orderInfo.intUserID != userId)
                        {
                            result.status = Core.Enums.MResultStatus.LogicError;
                            result.msg = "该订单不属于次用户！";
                            return result;
                        }
                        #endregion

                        #region 验正 订单状态
                        if (orderInfo.intOrderState < 0)
                        {
                            result.status = Core.Enums.MResultStatus.LogicError;
                            result.msg = "订单状态错误！";
                            return result;
                        }
                        #endregion

                        #region 验证 订单支付状态
                        if (orderInfo.intPayState != 0)
                        {
                            result.status = Core.Enums.MResultStatus.LogicError;
                            result.msg = "订单支付状态错误！";
                            return result;
                        }
                        #endregion

                        var payInfo = baseDataDal.GetPaymentInfo(payId);

                        #region 验证支付信息
                        if (payInfo == null)
                        {
                            result.status = Core.Enums.MResultStatus.LogicError;
                            result.msg = "支付方式错误！";
                            return result;
                        }
                        #endregion

                        var payCofnig = new PayConfigs()
                                            {
                                                OutTradeNo = string.Format("{0}-{1}", orderInfo.vchOrderCode, orderInfo.vchUserCode),
                                                OutUser = memberInfo.email,
                                                Subject = "母婴之家订单支付",
                                                RequestIdentity = string.Format("{0}_{1}", orderInfo.intUserID, memberInfo.email),
                                                TotalFee = orderInfo.numReceAmount.ToString(CultureInfo.InvariantCulture)
                                            };

                        switch (payInfo.intPayID)
                        {
                            #region 支付宝（手机）支付
                            case 20049:
                                {
                                    var paymentManage = new AlipayWapPayment(payCofnig);
                                    result.info = paymentManage.Init().CreateRequestUrl();
                                    result.status = Core.Enums.MResultStatus.Success;
                                    break;
                                }
                            #endregion

                            #region 默认值
                            default:
                                result.status = Core.Enums.MResultStatus.LogicError;
                                result.msg = "该支付方式wcf 不支持！ 请联系客服！";
                                break;
                            #endregion
                        }

                    }
                    else
                    {
                        result.status = Core.Enums.MResultStatus.LogicError;
                        result.msg = "订单编码错误！";
                    }
                }
                else
                {
                    result.status = Core.Enums.MResultStatus.ParamsError;
                    result.msg = "参数错误！";
                }
            }
            catch (Exception)
            {
                result.status = Core.Enums.MResultStatus.ExceptionError;
                result.msg = "订单支付 执行出现异常！";
            }

            return result;
        }
    }
}
