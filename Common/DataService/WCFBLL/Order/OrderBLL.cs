using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;

using Core.DataType;
using Core.DataTypeUtility;
using Core.Enums;
using Core.LogUtility;
using EF.Model.DataContext;
using Factory;
using Wcf.BLL.BaseData;
using Wcf.BLL.Goods;
using Wcf.BLL.ServiceReference.External;
using Wcf.Entity.Enum;
using Wcf.Entity.Order;

namespace Wcf.BLL.Order
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderBLL
    {
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="channelId"></param>
        /// <param name="uid"></param>
        /// <param name="userId"></param>
        /// <param name="orderEntity"></param>
        /// <returns></returns>
        public static MResult<ItemOrder> CreateOrder(string guid, int channelId, string uid, int userId, OrderEntity orderEntity)
        {
            var result = new MResult<ItemOrder>(true);

            try
            {
                var memberDal = DALFactory.Member();
                var baseDataDal = DALFactory.BaseData();
                var shoppingCartDal = DALFactory.ShoppingCartDal();
                var orderDal = DALFactory.Order();

                var payId = MCvHelper.To<int>(orderEntity.payid, -1);

                #region 验证数据

                #region 用户是否登录
                if (userId <= 0)
                {
                    result.msg = "请登录后再操作！";
                    result.status = MResultStatus.ParamsError;
                    return result;
                }
                #endregion

                #region 收货地址
                if (orderEntity.addressid <= 0)
                {
                    result.msg = "请选择收货地址！";
                    result.status = MResultStatus.ParamsError;
                    return result;
                }
                #endregion

                #region 支付方式
                if (payId < 0)
                {
                    result.msg = "请选择支付方式！";
                    result.status = MResultStatus.ParamsError;
                    return result;
                }
                #endregion

                #region 配送方式
                if (orderEntity.logisticsid <= 0)
                {
                    result.msg = "请选择配送方式！";
                    result.status = MResultStatus.ParamsError;
                    return result;
                }
                #endregion

                #region 发票信息是否完整
                if (orderEntity.titletype == null)
                {
                    result.msg = "请选择发票类型！";
                    result.status = MResultStatus.ParamsError;
                    return result;
                }
                if (orderEntity.titletype != Entity.Enum.Invoice.TitleType.NoNeed)
                {
                    if (orderEntity.invoicecategory == null)
                    {
                        result.msg = "请选择发票分类！";
                        result.status = MResultStatus.ParamsError;
                        return result;
                    }
                    if (orderEntity.titletype == Entity.Enum.Invoice.TitleType.Company && string.IsNullOrEmpty(orderEntity.invoicetitle))
                    {
                        result.msg = "请填写发票抬头！";
                        result.status = MResultStatus.ParamsError;
                        return result;
                    }
                }
                #endregion

                #endregion

                var memberInfo = memberDal.GetMemberInfo(uid);

                #region 验证用户是否存在
                if (memberInfo == null || memberInfo.membNo <= 0)
                {
                    result.msg = "该用户不存在！";
                    result.status = MResultStatus.ParamsError;
                    return result;
                }
                #endregion

                //购物车商品数据
                List<ShoppingCartEntity> norMalShoppingCartList = null;

                #region 判断购物车是否有商品
                var shoppingCartList = shoppingCartDal.GetShoppingCartProductInfosByUserIDGuidChannelID(userId, guid, channelId);
                if (shoppingCartList == null || !shoppingCartList.Any())
                {
                    result.msg = "购物车没有商品！";
                    result.status = MResultStatus.LogicError;
                    return result;
                }
                norMalShoppingCartList = (from a in shoppingCartList
                                          where a.intIsDelete == 0
                                          select a).ToList();

                if (!norMalShoppingCartList.Any())
                {
                    result.msg = "购物车没有商品！";
                    result.status = MResultStatus.LogicError;
                    return result;
                }
                #endregion

                #region 该用户是否是黑名单
                var isExistBacklist = memberDal.CheckUserIdInBackList(userId);
                if (isExistBacklist)
                {
                    result.msg = "您的用户出现异常，请联系我们的客服人员进行解决！";
                    result.status = MResultStatus.LogicError;
                    return result;
                }
                #endregion

                //收货地址信息
                var addressInfo = memberDal.GetMemberAddressInfo(orderEntity.addressid);

                #region 验证收货地址
                #region 是否存在
                if (addressInfo == null || addressInfo.intAddressID <= 0 || addressInfo.intCityID <= 0 && payId <= 0 && orderEntity.logisticsid <= 0)
                {
                    result.msg = "收货地址信息不正确！";
                    result.status = MResultStatus.ParamsError;
                    return result;
                }
                #endregion
                #endregion

                //配送方式
                var deliverInfo = baseDataDal.GetDeliverInfo(orderEntity.logisticsid);

                #region 验证配送方式
                if (deliverInfo == null || deliverInfo.intDeliverID == 0)
                {
                    result.msg = "验证配送方式信息不正确！";
                    result.status = MResultStatus.ParamsError;
                    return result;
                }
                #endregion

                #region 检查商品销售区域
                var checkGoodsSaleAreaState = CheckGoodsSaleArea(norMalShoppingCartList, userId, MCvHelper.To<int>(addressInfo.intCityID), channelId);
                if (checkGoodsSaleAreaState.Any())
                {
                    result.msg = "有部分商品不在您选择的区域内销售！";
                    result.status = MResultStatus.LogicError;
                    result.data = String.Join(",", checkGoodsSaleAreaState.ToArray());
                    return result;
                }
                #endregion

                var summaryOrderInfo = SummaryOrderInfo(norMalShoppingCartList, channelId, userId,
                    payId, orderEntity.logisticsid,
                                                     MCvHelper.To<int>(addressInfo.intCityID));

                #region 开始创建订单
                if (summaryOrderInfo != null && summaryOrderInfo.TotalGoodsFee > 0)
                {
                    var order = new Sale_Order();

                    #region 订单主表信息
                    order.dtCreateDate = DateTime.Now;
                    order.dtSendDate = CheckDateTime(orderEntity.posttimetype, orderEntity.logisticsid);                         //处理送货日期
                    order.intChannel = channelId;
                    order.intCreaterID = 555;
                    order.intDeliverID = orderEntity.logisticsid;
                    order.intLogisticsID = 21;
                    order.intOrderState = 1;
                    order.intOrderType = 1;
                    order.intPayID = payId;
                    order.intPayState = 0;
                    order.intStockID = 100;
                    order.intTotalStars = summaryOrderInfo.TotalScore;
                    order.intUserID = userId;
                    order.numAddAmount = 0;
                    order.numCarriage = summaryOrderInfo.TotalFreight;
                    order.numChange = 0;
                    order.numGoodsAmount = summaryOrderInfo.TotalGoodsFee;
                    order.numCouponAmount = summaryOrderInfo.TotalDiscountFee;
                    order.numReceAmount = summaryOrderInfo.TotalOrderFee;
                    order.numWeight = summaryOrderInfo.TotalWeight;
                    order.vchSendTime = order.dtSendDate.ToShortTimeString();
                    order.vchUserCode = memberInfo.userCode;
                    order.vchOrderCode = GetOrderCode();
                    #endregion

                    #region 配送信息

                    var deliver = new Sale_Order_Deliver();
                    deliver.intAddressID = addressInfo.intAddressID;
                    deliver.intCityID = MCvHelper.To<int>(addressInfo.intCityID, 0);
                    deliver.vchCityName = addressInfo.vchCityName;
                    deliver.vchConsignee = addressInfo.vchConsignee;
                    deliver.intCountyID = MCvHelper.To<int>(addressInfo.intCountyID, 0);
                    deliver.vchCountyName = addressInfo.vchCountyName;
                    deliver.vchDetailAddr = addressInfo.vchStateName + "," + addressInfo.vchCityName + "," + addressInfo.vchCountyName + "," + addressInfo.vchDetailAddr;
                    deliver.vchHausnummer = addressInfo.vchHausnummer;
                    deliver.vchMobile = addressInfo.vchMobile;
                    deliver.vchPhone = addressInfo.vchPhone;
                    deliver.vchPostCode = addressInfo.vchPostCode;
                    deliver.vchRoadName = addressInfo.vchRoadName;
                    deliver.intStateID = MCvHelper.To<int>(addressInfo.intStateID, 0);
                    deliver.vchStateName = addressInfo.vchStateName;
                    deliver.vchUserMemo = orderEntity.remark;
                    deliver.vchOrderCode = order.vchOrderCode;

                    #endregion

                    #region 发票信息

                    var invoice = new Sale_Order_Invoice();
                    if (orderEntity.titletype != null && orderEntity.titletype != Invoice.TitleType.NoNeed)
                    {
                        if (orderEntity.titletype == Invoice.TitleType.Personal)
                        {
                            invoice.vchInvoicTitile = "个人";
                        }
                        else if (orderEntity.titletype == Invoice.TitleType.Company)
                        {
                            invoice.vchInvoicTitile = orderEntity.invoicetitle;
                        }

                        invoice.intInvoiceType = (int)orderEntity.invoicecategory;
                        invoice.intInvoiceKind = 1;
                        invoice.numAmount = order.numReceAmount;
                        invoice.dtBillingTime = DateTime.Now;
                        invoice.dtCreateDate = DateTime.Now;
                        invoice.intIsBilling = 1;
                        invoice.intIsDetail = 0;
                        invoice.vchOrderCode = order.vchOrderCode;
                        invoice.vchPhone = addressInfo.vchPhone;
                        invoice.intUserID = userId;
                    }

                    #endregion

                    #region 保存订单
                    string message;
                    result.info.oid = orderDal.SaveWebOrder(order, invoice, deliver, null, userId, guid, channelId, MCvHelper.To<int>(memberInfo.clusterId, 0), -1, out message);
                    if (result.info.oid > 0)
                    {
                        #region 清空购物车
                        shoppingCartDal.ClearShoppingCart(userId);
                        #endregion

                        #region 同步订单信息到 BBHome
                        orderDal.SyncOrderInfoToBBHome(result.info.oid.ToString());
                        #endregion

                        result.status = MResultStatus.Success;

                        var payType = string.Empty;
                        if (payId == 0)
                            payType = "货到付款";
                        else if (payId == 1)
                            payType = "在线支付";
                        var postTimetype = string.Empty;
                        switch (orderEntity.posttimetype)
                        {
                            case 1:
                                postTimetype = "工作日送货";
                                break;
                            case 2:
                                postTimetype = "工作日、双休日均可送货";
                                break;
                            case 3:
                                postTimetype = "只双休日送货";
                                break;
                        }

                        result.info.ocode = order.vchOrderCode;
                        result.info.paytype = payType;
                        result.info.logisticstype = deliverInfo.vchDeliverName;
                        result.info.total_fee = order.numGoodsAmount;
                        result.info.total_freight = order.numCarriage;
                        result.info.total_order = order.numReceAmount;
                        result.info.posttimetype = postTimetype;
                    }

                    #endregion

                }
                #endregion
            }
            catch (Exception ex)
            {
                result.status = MResultStatus.ExecutionError;
                MLogManager.Error(MLogGroup.Order.创建订单, null, "", ex);
            }

            return result;
        }

        /// <summary>
        /// 获取临时订单信息
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="channelId"> </param>
        /// <param name="uid"></param>
        /// <param name="userId"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static MResult<OrderResult> GetTempOrderInfo(string guid, int channelId, string uid, int userId, OrderEntity order)
        {
            var result = new MResult<OrderResult>(true);

            try
            {
                if (order.addressid > 0)
                {
                    var memberDal = DALFactory.Member();
                    var shoppingCartDal = DALFactory.ShoppingCartDal();
                    //获取用户选择的收货地址
                    var addressInfo = memberDal.GetMemberAddressInfo(order.addressid);
                    //判断地址是否存在，并且判断 支付方式和配送方式是否已选择
                    if (addressInfo != null && addressInfo.intCityID > 0 && order.payid != null && order.logisticsid > 0)
                    {
                        //查询该用户购物车所有商品
                        var shoppingCartList = shoppingCartDal.GetShoppingCartProductInfosByUserIDGuidChannelID(userId, guid, channelId);
                        if (shoppingCartList.Any())
                        {
                            //排除 已删除的商品
                            var notDelShoppingCart = (from a in shoppingCartList where a.intIsDelete == 0 select a).ToList();
                            if (notDelShoppingCart.Any())
                            {
                                var summaryOrderInfo = SummaryOrderInfo(notDelShoppingCart, channelId, userId, MCvHelper.To<int>(order.payid, -1), order.logisticsid,
                                                 MCvHelper.To<int>(addressInfo.intCityID));
                                if (summaryOrderInfo != null)
                                {
                                    result.info.total_discount_fee = summaryOrderInfo.TotalDiscountFee;
                                    result.info.total_freight = summaryOrderInfo.TotalFreight;
                                    result.info.total_goods_fee = summaryOrderInfo.TotalGoodsFee;
                                    result.info.total_order_fee = summaryOrderInfo.TotalOrderFee;
                                    result.info.total_original = summaryOrderInfo.TotalOriginal;
                                    result.info.total_score = summaryOrderInfo.TotalScore;
                                    result.info.total_weight = summaryOrderInfo.TotalWeight;
                                }
                            }
                            else
                            {
                                result.status = Core.Enums.MResultStatus.LogicError;
                                result.msg = "购物车没有商品！";
                            }
                        }
                        else
                        {
                            result.status = Core.Enums.MResultStatus.LogicError;
                            result.msg = "购物车没有商品！";
                        }
                    }
                    else
                    {
                        result.status = Core.Enums.MResultStatus.LogicError;
                        result.msg = "请选择支付方式和配送方式！";
                    }
                }
                else
                {
                    result.status = Core.Enums.MResultStatus.LogicError;
                    result.msg = "请选择收货地址！";
                }
            }
            catch (Exception ex)
            {
                result.status = MResultStatus.ExecutionError;
                MLogManager.Error(MLogGroup.Order.获取临时订单信息, null, "", ex);
            }

            return result;
        }

        /// <summary>
        /// 订单汇总信息
        /// </summary>
        /// <param name="norMalShoppingCart">购物车列表</param>
        /// <param name="channelId">区域ID</param>
        /// <param name="userId">用户id</param>
        /// <param name="payId">支付id</param>
        /// <param name="logisticsId">配送id</param>
        /// <param name="cityId">城市id</param>
        /// <returns></returns>
        public static OrderSumaryEntity SummaryOrderInfo(List<ShoppingCartEntity> norMalShoppingCart, int channelId, int userId, int payId, int logisticsId, int cityId)
        {
            var result = new OrderSumaryEntity();
            try
            {
                result.TotalScore = norMalShoppingCart.Sum(c => c.intScore * c.intBuyCount);
                result.TotalGoodsFee = norMalShoppingCart.Sum(c => c.intBuyCount * c.numSalePrice);
                result.TotalWeight = norMalShoppingCart.Sum(c => c.intBuyCount * c.intWeight ?? 0);
                result.TotalOriginal = norMalShoppingCart.Sum(c => c.intBuyCount * c.numOrgPrice);
                result.TotalFreight =
                    BaseDataBLL.GetLogisticsInfo(
                                                channelId,
                                                userId,
                                                MCvHelper.To<int>(cityId, 0),
                                                payId, logisticsId,
                                                result.TotalWeight, result.TotalGoodsFee).info;
                result.TotalDiscountFee = result.TotalGoodsFee -
                                    result.TotalOriginal;
                result.TotalOrderFee = result.TotalFreight +
                                              result.TotalGoodsFee -
                                              result.TotalDiscountFee;
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Order.订单汇总信息, null, "", ex);
            }
            return result;
        }

        /// <summary>
        /// 检查商品销售区域
        /// </summary>
        /// <param name="shoppingList"> </param>
        /// <param name="userId"></param>
        /// <param name="cityId"></param>
        /// <param name="channelId"></param>
        /// <returns>无效的 商品ID</returns>
        public static List<int> CheckGoodsSaleArea(List<ShoppingCartEntity> shoppingList, int userId, int cityId, int channelId)
        {
            var result = new List<int>();

            try
            {
                var orderDal = DALFactory.Order();
                var norMalShoppingCartIdList = orderDal.CheckGoodsSaleArea(userId, cityId, channelId);
                shoppingList.ForEach(item =>
                                         {
                                             if (!norMalShoppingCartIdList.Contains(item.intShopCartID))
                                                 result.Add(item.intProductID);
                                         });
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Order.检查商品销售区域, null, "", ex);
            }

            return result;
        }

        /// <summary>
        /// 计算送货时间
        /// </summary>
        /// <param name="num"></param>
        /// <param name="deliverID"></param>
        /// <returns></returns>
        private static DateTime CheckDateTime(int num, int deliverID)
        {
            //获取当前日期是星期几
            string dt = DateTime.Today.DayOfWeek.ToString();
            DateTime week = DateTime.Now.Date;
            int count = 0;
            int max = 1;

            if (deliverID == 1)
            {
                if (num == 1)
                {
                    #region

                    for (int i = 0; i < 7; i++)
                    {
                        dt = DateTime.Today.AddDays(max).DayOfWeek.ToString();

                        //根据取得的星期英文单词返回汉字
                        switch (dt)
                        {
                            case "Monday":
                                count = 1;
                                break;
                            case "Tuesday":
                                count = 1;
                                break;
                            case "Wednesday":
                                count = 1;
                                break;
                            case "Thursday":
                                count = 1;
                                break;
                            case "Friday":
                                count = 1;
                                break;
                            default:
                                count = 0;
                                break;
                        }

                        if (count == 1)
                        {
                            break;
                        }
                        max = max + 1;
                    }

                    #endregion
                }

                if (num == 3)
                {
                    #region

                    for (int j = 0; j < 7; j++)
                    {
                        dt = DateTime.Today.AddDays(max).DayOfWeek.ToString();
                        switch (dt)
                        {
                            case "Saturday":
                                count = 1;
                                break;
                            case "Sunday":
                                count = 1;
                                break;
                            default:
                                count = 0;
                                break;
                        }

                        if (count == 1)
                        {
                            break;
                        }
                        max = max + 1;
                    }

                    #endregion
                }
            }

            week = DateTime.Now.AddDays(max).Date;

            //21点之后下单，送货日期如果是明天则自动延伸至后天
            if (DateTime.Now.Hour >= 21 && week <= DateTime.Now.AddDays(1).Date)
            {
                week = week.AddDays(1);
            }

            if (deliverID == 1)
            {
                string addt = week.ToString();
                addt = addt.Substring(0, addt.Length - 2);
                if (num == 1)
                {
                    week = Convert.ToDateTime(addt + "1");
                }
                if (num == 2)
                {
                    week = Convert.ToDateTime(addt + "2");
                }
                if (num == 3)
                {
                    week = Convert.ToDateTime(addt + "3");
                }
            }

            return week;
        }

        /// <summary>
        /// 获取订单编号
        /// </summary>
        /// <returns></returns>
        public static string GetOrderCode()
        {
            var result = string.Empty;
            using (var wmsvClient = new BbHomeServiceClient())
            {
                try
                {
                    wmsvClient.Open();
                    result = wmsvClient.GetOrderCode();
                    wmsvClient.Close();
                }
                catch
                {

                }
            }
            return result;
        }

        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="uid"></param>
        /// <param name="userId"></param>
        /// <param name="orderCode"></param>
        /// <returns></returns>
        public static MResult<ItemOrderDetails> GetOrderinfo(int sid, string uid, int userId, string orderCode)
        {
            var result = new MResult<ItemOrderDetails>();

            try
            {
                #region 参数判断
                if (userId <= 0)
                {
                    result.status = MResultStatus.ParamsError;
                    result.msg = "用户标识错误！";
                }
                if (string.IsNullOrEmpty(orderCode))
                {
                    result.status = MResultStatus.ParamsError;
                    result.msg = "订单标识错误！";
                }
                #endregion

                var orderDal = DALFactory.Order();
                var baseDataDal = DALFactory.BaseData();

                var orderInfo = orderDal.GetOrderInfo(userId, orderCode);
                if (orderInfo != null && orderInfo.OrderNo > 0)
                {
                    orderInfo.PayType = orderInfo.PayId == 0 ? "货到付款" : "在线支付";

                    orderInfo.InvoiceType = (int)(string.IsNullOrEmpty(orderInfo.InvoiceTitle)
                                                 ? Invoice.TitleType.NoNeed
                                                 : orderInfo.InvoiceTitle.StartsWith("个人")
                                                       ? Invoice.TitleType.Personal
                                                       : Invoice.TitleType.Company);
                    orderInfo.PayStatus = orderInfo.PayStatusId == 2 ? "已付款" : "未付款";
                    #region 订单状态
                    string orderStatus;
                    switch (orderInfo.OrderStatusId)
                    {
                        case 0: orderStatus = "付款未审核"; break;
                        case 1: orderStatus = "未确定"; break;
                        case 4: orderStatus = "客户已确认"; break;
                        case 5: orderStatus = "生成配货单"; break;
                        case 7: orderStatus = "已出库"; break;
                        case 20: orderStatus = "完成"; break;
                        default:
                            orderStatus = "未知"; break;
                    }
                    #endregion
                    orderInfo.OrderStatus = orderStatus;

                    var invoiceCategory = MCvHelper.To<int>(orderInfo.InvoiceCategory, 0);

                    result.info = new ItemOrderDetails
                                         {
                                             oid = orderInfo.OrderNo,
                                             ocode = orderInfo.OrderCode,
                                             status = orderInfo.OrderStatus,
                                             addr = orderInfo.AddressInfo,
                                             province = orderInfo.Provinces,
                                             city = orderInfo.City,
                                             county = orderInfo.County,
                                             contact_name = orderInfo.Consignee,
                                             invoicecategory = invoiceCategory,
                                             invoicetitle = orderInfo.InvoiceTitle,
                                             phone = orderInfo.Phone,
                                             titletype = orderInfo.InvoiceType,
                                             mobile = orderInfo.Mobile,
                                             paytype = orderInfo.PayType,
                                             statusid = orderInfo.OrderStatusId,
                                             zip = orderInfo.Zip,
                                             paystatus = orderInfo.PayStatus,
                                             paystatusid = orderInfo.PayStatusId,
                                             deliverytype = orderInfo.DeliveryType
                                         };
                    result.status = MResultStatus.Success;
                }
                else
                {
                    result.status = MResultStatus.Undefined;
                    result.msg = "没有数据！";
                }
            }
            catch (Exception ex)
            {
                result.status = MResultStatus.ExecutionError;
                MLogManager.Error(MLogGroup.Order.获取订单信息, null, "获取订单信息", ex);
            }

            return result;
        }

        /// <summary>
        /// 获取订单商品列表
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="uid"></param>
        /// <param name="userId"></param>
        /// <param name="orderCode"></param>
        /// <returns></returns>
        public static MResultList<ItemOrderGoods> GetOrderGoodsList(int sid, string uid, int userId, string orderCode)
        {
            var result = new MResultList<ItemOrderGoods>(true);

            try
            {
                #region 参数判断
                if (userId <= 0)
                {
                    result.status = MResultStatus.ParamsError;
                    result.msg = "用户标识错误！";
                }
                if (string.IsNullOrEmpty(orderCode))
                {
                    result.status = MResultStatus.ParamsError;
                    result.msg = "订单标识错误！";
                }
                #endregion

                var orderDal = DALFactory.Order();
                var memberDal = DALFactory.Member();

                var memberInfo = memberDal.GetMemberInfo(userId);
                if (memberInfo == null || memberInfo.membNo <= 0)
                {
                    result.status = MResultStatus.Undefined;
                    result.msg = "用户不存在！";
                }
                var clusterId = MCvHelper.To(memberInfo.clusterId, 1);

                var orderGoodsList = orderDal.GetOrderGoodsList(userId, orderCode, clusterId);
                if (orderGoodsList.Any())
                {
                    orderGoodsList.ForEach(item =>
                                               {
                                                   var goodsItem = new ItemOrderGoods()
                                                                       {
                                                                           gid = item.intProductID,
                                                                           title = item.vchProductName,
                                                                           price = item.numSalePrice,
                                                                           num = item.intQty,
                                                                           total = item.numTotalAmount,
                                                                           pic_url = GoodsBLL.FormatProductPicUrl(item.PicUrl),
                                                                           score = item.intScores,
                                                                           marketprice = MCvHelper.To<decimal>(item.numStandarPrice, item.numSalePrice),
                                                                           productcode = item.vchProductPrinted
                                                                       };
                                                   result.list.Add(goodsItem);
                                               });
                    result.status = MResultStatus.Success;
                }
                else
                {
                    result.status = MResultStatus.Undefined;
                    result.msg = "没有数据！";
                }

            }
            catch (Exception ex)
            {
                result.status = MResultStatus.ExecutionError;
                MLogManager.Error(MLogGroup.Order.获取订单信息, null, "获取订单信息", ex);
            }

            return result;
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="uid"></param>
        /// <param name="userId"></param>
        /// <param name="begimTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static MResultList<ItemOrder> GetOrdersList(int sid, string uid, int userId, DateTime begimTime, DateTime endTime)
        {
            var result = new MResultList<ItemOrder>(true);

            #region 参数验证

            if (userId <= 0)
            {
                result.status = MResultStatus.ParamsError;
                result.msg = "用户标识错误！";
            }

            #endregion

            try
            {
                var orderDal = DALFactory.Order();

                var orderList = orderDal.GetOrdersList(userId, begimTime, endTime);
                if (orderList.Any())
                {
                    orderList.ForEach(item =>
                                          {
                                              var payType = item.intPayID == 0 ? "货到付款" : "在线支付";

                                              var payStatus = item.intPayState == 2 ? "已付款" : "未付款";
                                              #region 订单状态
                                              string orderStatus;
                                              switch (item.intOrderState)
                                              {
                                                  case 0: orderStatus = "付款未审核"; break;
                                                  case 1: orderStatus = "未确定"; break;
                                                  case 4: orderStatus = "客户已确认"; break;
                                                  case 5: orderStatus = "生成配货单"; break;
                                                  case 7: orderStatus = "已出库"; break;
                                                  case 20: orderStatus = "完成"; break;
                                                  default:
                                                      orderStatus = "未知"; break;
                                              }
                                              #endregion
                                              var orderInfo = new ItemOrder
                                                                  {
                                                                      oid = item.intOrderNO,
                                                                      ocode = item.vchOrderCode,
                                                                      created = item.dtCreateDate,
                                                                      statusid = item.intOrderState,
                                                                      paytype = payType,
                                                                      status = orderStatus,
                                                                      total_fee = item.numGoodsAmount,
                                                                      total_freight = item.numCarriage,
                                                                      total_order = item.numReceAmount
                                                                  };
                                              result.list.Add(orderInfo);
                                          });
                    result.status = MResultStatus.Success;
                }

            }
            catch (Exception ex)
            {
                result.status = MResultStatus.ExecutionError;
                MLogManager.Error(MLogGroup.Order.获取订单列表, null, "获取订单列表", ex);
            }

            return result;
        }
    }
}
