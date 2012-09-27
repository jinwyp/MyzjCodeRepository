using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using Core.DataType;
using EF.Model;
using EF.Model.DataContext;
using System.Data.Objects;
using System.Data;
using Core.DataTypeUtility;
using EF.Model.Entity;
using Wcf.Entity.Enum;

namespace EF.DAL
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Order
    {
        /// <summary>
        /// 检查商品销售区域
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cityId"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public List<int> CheckGoodsSaleArea(int userId, int cityId, int channelId)
        {
            List<int> result;

            using (var db = new HolycaEntities())
            {
                var queryTxt = from a in db.Sale_ShoppingCart
                               join b in db.Base_SaleArea_Product on a.intProductID equals b.intProductID
                               join c in db.Base_Region on b.intAreaID equals c.intAreaID
                               where a.intUserID == userId && c.intRegionID == cityId && b.intState == 1 && a.intChannelID == channelId
                               select a.intShopCartID;
                result = queryTxt.Distinct().ToList();
            }

            return result;
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="order"></param>
        /// <param name="invoice"></param>
        /// <param name="deliver"></param>
        /// <param name="orderProm"></param>
        /// <param name="userId"></param>
        /// <param name="guid"></param>
        /// <param name="channelId"></param>
        /// <param name="clusterId"></param>
        /// <param name="promID"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public int SaveWebOrder(Sale_Order order, Sale_Order_Invoice invoice, Sale_Order_Deliver deliver, Sale_Order_Prom_Rela orderProm,
            int userId, string guid, int channelId, int clusterId, int promID, out string message)
        {
            var orderNo = -1;
            message = "";
            var order_rela = new List<Sale_Order_Prom_Rela>();
            using (var ctx = new HolycaEntities())
            {
                using (var cart = new HolycaEntities())
                {
                    ctx.Connection.Open();
                    var tran = ctx.Connection.BeginTransaction();
                    try
                    {
                        //保存订单主表
                        ctx.Sale_Order.AddObject(order);
                        //先提交得到订单编号
                        ctx.SaveChanges();
                        //返回订单编号
                        orderNo = order.intOrderNO;

                        #region 保存发票表
                        if (invoice != null)
                        {
                            invoice.vchOrderCode = order.vchOrderCode;
                            invoice.dtCreateDate = DateTime.Now;
                            invoice.dtBillingTime = DateTime.Now;
                            ctx.Sale_Order_Invoice.AddObject(invoice);
                        }
                        #endregion

                        Sale_Order_Detail item = null;
                        //获取购物车信息
                        var all = from cartItem in cart.Sale_ShoppingCart where cartItem.intUserID == userId && cartItem.intChannelID == channelId && cartItem.intIsDelete == 0 select cartItem;
                        var v = from allitem in all where (allitem.intProductType == null || allitem.intProductType == 0) select allitem;
                        if (v.Any())
                        {
                            decimal totalMoney = 0;
                            foreach (var cartItem in v)
                            {
                                #region 购物车转订单明细
                                item = new Sale_Order_Detail();

                                item.numTotalAmount = ((decimal?)cartItem.numSalePrice ?? 0) * cartItem.intBuyCount;
                                if (invoice != null)
                                    item.intIsInvoice = 1;
                                else
                                    item.intIsInvoice = 0;
                                item.numCleanCost = 0;
                                item.numCost = 0;
                                item.intBaseStar = (int?)cartItem.intOrgScore ?? 0;
                                item.intUserID = userId;
                                item.dtOpDate = DateTime.Now;
                                item.vchOrderCode = order.vchOrderCode;
                                item.numStandarPrice = cartItem.numOrgPrice;
                                item.numSalePrice = cartItem.numSalePrice;
                                item.intHerdPriceID = clusterId;
                                item.intProductID = cartItem.intProductID;
                                item.vchProductName = cartItem.nchProductName;
                                item.vchProductPrinted = cartItem.vchProductPrinted;
                                if (promID > 0)
                                {
                                    if (cartItem.intPromID > 0)
                                    {
                                        item.intPromID = cartItem.intPromID ?? 0;
                                    }
                                    else
                                    {
                                        item.intPromID = promID;
                                    }
                                }
                                else
                                {
                                    item.intPromID = cartItem.intPromID ?? 0;
                                }
                                item.intQty = cartItem.intBuyCount;
                                item.intRtnQty = 0;
                                item.intScores = (cartItem.intScore == 0 ? 0 : cartItem.intOrgScore) * cartItem.intBuyCount;
                                item.intChannel = channelId;
                                item.intStockID = order.intStockID;
                                //修复ckid为空的情况
                                item.intLogisticsID = 21;
                                ctx.Sale_Order_Detail.AddObject(item);
                                totalMoney += item.numTotalAmount;
                                #endregion
                            }

                            #region 促销处理
                            /*
                            PromOrderRelation orderrelation = null;
                            //保存满减促销的关系
                            foreach (Sale_ShoppingCart cartItem in p)
                            {
                                orderrelation = new PromOrderRelation();
                                orderrelation.vchOrderCode = order.vchOrderCode;
                                orderrelation.intPromID = (int)cartItem.intPromID;
                                orderrelation.numCouponAmount = cartItem.numSalePrice;
                                ctx.PromOrderRelation.InsertOnSubmit(orderrelation);
                            }

                            //修改已经保存的满减和购物车关系中的订单号
                            //var productrelation= from r in ctx.PromOrderProductRelation 
                            //       from q in v 
                            //       where r.intShopCartID  == q.intShopCartID 
                            //       select r;
                            string intshopcartids = ",";
                            foreach (Sale_ShoppingCart productid in v)
                            {
                                intshopcartids = intshopcartids + productid.intShopCartID.ToString() + ",";
                            }

                            var productrelation = ctx.PromOrderProductRelation.Where(c => intshopcartids.IndexOf("," + c.intShopCartID.ToString() + ",") >= 0).ToList();
                            //保存满减促销的关系
                            foreach (PromOrderProductRelation pre in productrelation)
                            {
                                pre.vchOrderCode = order.vchOrderCode;
                                ctx.SubmitChanges();
                            }
                            */

                            #endregion

                            #region 货款+运费
                            if (order.numGoodsAmount != totalMoney - order.numCouponAmount || order.numReceAmount != totalMoney + order.numCarriage - order.numCouponAmount)
                            {
                                order.numGoodsAmount = totalMoney;
                                order.numReceAmount = totalMoney + order.numCarriage;
                            }
                            #endregion

                            #region 保存支付状态
                            var orderPayStatus = new Sale_Order_PayState();
                            orderPayStatus.intUserID = 555; //顾客
                            orderPayStatus.dtOpdate = DateTime.Now;
                            orderPayStatus.vchOrderCode = order.vchOrderCode;
                            orderPayStatus.intPayState = 0; //未付款
                            ctx.AddToSale_Order_PayState(orderPayStatus);
                            #endregion

                            #region 保存订单状态
                            var orderStatus = new Sale_Order_State();
                            orderStatus.intUserID = 555; //顾客
                            orderStatus.intOrderState = 1; //新建
                            orderStatus.dtOpdate = DateTime.Now;
                            orderStatus.vchOrderCode = order.vchOrderCode;
                            ctx.Sale_Order_State.AddObject(orderStatus);
                            #endregion

                            #region 保存销售订单配送信息
                            ctx.Sale_Order_Deliver.AddObject(deliver);
                            ctx.SaveChanges();
                            #endregion

                            #region 保存订单和促销关系
                            foreach (var rela_item in v)
                            {
                                var shopcart_rela = cart.Sale_ShopCart_Prom_Rela.FirstOrDefault(c => c.intShopCartID == rela_item.intShopCartID);
                                if (shopcart_rela != null)
                                {
                                    orderProm = new Sale_Order_Prom_Rela();
                                    orderProm.vchOrderCode = order.vchOrderCode;
                                    orderProm.intChannelID = channelId;
                                    orderProm.intUserID = userId;
                                    orderProm.intProductID = rela_item.intProductID;
                                    orderProm.intPromID = shopcart_rela.intPromID ?? 0;
                                    orderProm.intDetailID = item.intDetailID;
                                    orderProm.dtCreateDate = DateTime.Now;
                                    ctx.Sale_Order_Prom_Rela.AddObject(orderProm);
                                }
                            }
                            #endregion

                            //提交所有更改
                            tran.Commit();

                        }
                    }
                    catch (Exception e)
                    {
                        orderNo = -1;
                        message = e.Message;
                        tran.Rollback();
                    }
                }
            }
            return orderNo;
        }

        /// <summary>
        /// 同步订单信息到 BBHome
        /// </summary>
        /// <param name="orderCode"></param>
        /// <returns></returns>
        public bool SyncOrderInfoToBBHome(string orderCode)
        {
            using (var db = new HolycaEntities())
            {
                var objectParameter = new ObjectParameter("result", DbType.Int32);
                db.Up_Syn_ToBBHome_Now(orderCode, objectParameter);
                return MCvHelper.To<int>(objectParameter.Value, -1) != -1;
            }
        }

        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orderCode"></param>
        /// <returns></returns>
        public OrderDetails_ExtInfo GetOrderInfo(int userId, string orderCode)
        {
            using (var db = new bbHomeEntities())
            {
                var queryTxt = from a in db.sale_v_order
                               join b in db.base_t_member on a.userCode equals b.userCode
                               join c in db.sale_t_orderInvoice on a.invoiceNo equals c.invoiceNo
                               where b.membNo == userId && a.orderCode == orderCode
                               select new OrderDetails_ExtInfo
                                              {
                                                  OrderNo = a.orderNo,
                                                  OrderCode = a.orderCode,
                                                  Provinces = "",
                                                  City = "",
                                                  County = "",
                                                  AddressInfo = a.address,
                                                  Phone = a.mobile,
                                                  Mobile = a.mobile,
                                                  Consignee = a.sendTo,
                                                  InvoiceCategory = c.InvType,
                                                  InvoiceTitle = c.InvTitle,
                                                  Zip = a.zip,
                                                  PayStatusId = a.payStatus ?? 0,
                                                  OrderStatusId = a.flowStatus ?? 0,
                                                  DeliveryType = a.deliverName,
                                                  PayId = a.payMethod ?? 0
                                              };
                return queryTxt.FirstOrDefault();
            }
        }

        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="orderCode"></param>
        /// <returns></returns>
        public sale_v_order GetOrderInfo(string orderCode)
        {
            using (var db = new bbHomeEntities())
            {
                var queryTxt = from a in db.sale_v_order
                               where a.orderCode == orderCode
                               select a;
                var orderInfo = queryTxt.FirstOrDefault();
                return orderInfo;
            }
        }

        /// <summary>
        /// 获取商品
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orderCode"></param>
        /// <param name="clusterId"> </param>
        /// <returns></returns>
        public List<Sale_Order_Detail_And_GoodsInfo> GetOrderGoodsList(int userId, string orderCode, int clusterId)
        {
            using (var db = new bbHomeEntities())
            {
                var queryTxt = from a in db.sale_v_orderItem
                               join b in db.cg_t_products on a.productId equals b.productId
                               join c in db.sale_t_order on a.orderNo equals c.orderNo
                               where a.membNo == userId && c.orderCode == orderCode
                               select new Sale_Order_Detail_And_GoodsInfo
                                           {
                                               PicUrl = b.pictureUrl,
                                               intProductID = a.productId ?? 0,
                                               vchProductName = a.productName,
                                               numSalePrice = a.price ?? 0,
                                               intQty = a.qty ?? 0,
                                               intScores = a.scores ?? 0,
                                               numTotalAmount = a.amount ?? 0
                                           };
                return queryTxt.ToList();
            }
        }

        /// <summary>
        /// 查询订单配送信息
        /// </summary>
        /// <param name="orderCode"></param>
        /// <returns></returns>
        public Sale_Order_Deliver GetOrderDeliveryinfo(string orderCode)
        {
            using (var db = new HolycaEntities())
            {
                var queryTxt = from a in db.Sale_Order_Deliver
                               where a.vchOrderCode == orderCode
                               select a;
                return queryTxt.FirstOrDefault();
            }
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="begimTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<sale_v_order> GetOrdersList(int userId, DateTime begimTime, DateTime endTime)
        {
            //using (var db = new HolycaEntities())
            //{
            //    var queryTxt = from a in db.Sale_Order
            //                   where a.intUserID == userId && a.dtCreateDate <= endTime && a.dtCreateDate >= begimTime
            //                   orderby a.dtCreateDate descending
            //                   select a;
            //    return queryTxt.ToList();
            //}
            using (var db = new bbHomeEntities())
            {
                var queryTxt = from a in db.sale_v_order
                               join b in db.base_t_member on a.userCode equals b.userCode
                               where b.membNo == userId && a.createDate <= endTime && a.createDate >= begimTime
                               orderby a.createDate descending
                               select a;
                return queryTxt.ToList();
            }
        }

        /// <summary>
        /// 计算会员订单统计信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public MemberAccountInfo GetMemberOrderStatistics(int userId)
        {
            using (var db = new HolycaEntities())
            {
                var queryTxt = from a in db.Sale_Order
                               where a.intUserID == userId && a.intOrderState == 20
                               select new
                                          {
                                              a.numReceAmount,
                                              a.intTotalStars
                                          };
                var orderInfo = queryTxt.ToList();

                if (!orderInfo.Any()) return null;

                return new MemberAccountInfo
                           {
                               TotalIntegral = orderInfo.Sum(p => p.intTotalStars),
                               OrdersTotal = orderInfo.Sum(p => p.numReceAmount),
                               OrderCount = orderInfo.Count()
                           };
            }
        }

        /// <summary>
        /// 更新订单支付状态为成功
        /// </summary>
        /// <param name="orderCode"></param>
        /// <param name="userCode"></param>
        public void UpdateOrderPayStatusSuccess(string orderCode, string userCode)
        {
            using (var db = new HolycaEntities())
            {
                var result = new ObjectParameter("intResult", DbType.Int32);
                db.Up_Synchro_OrderPayState(orderCode, 0, 2, result);
            }
        }

        /// <summary>
        /// 获取订单所有状态
        /// </summary>
        /// <param name="oCodes"></param>
        /// <returns></returns>
        public List<OrderAllStatus> GetOrderAllStatusByOrderCodes(List<string> oCodes)
        {
            var result = new List<OrderAllStatus>();
            using (var db = new bbHomeEntities())
            {
                var queryTxt = from a in db.sale_t_order
                               join b in db.sale_t_orderStatus on a.orderNo equals b.orderNo
                               join c in db.sale_t_orderPayStatus on a.orderNo equals c.orderNo
                               where oCodes.Contains(a.orderCode)
                               select new OrderAllStatus
                                          {
                                              OrderCode = a.orderCode,
                                              OrderNo = a.orderNo,
                                              OrderStatus = b.flowStatus,
                                              PayStatus = c.payStatus
                                          };
                result = queryTxt.ToList();
            }
            return result;
        }

        /// <summary>
        /// 更新订单所有状态
        /// </summary>
        /// <param name="orderList"></param>
        public void UpdateOrderAllStatus(ref List<Sale_Order> orderList)
        {
            if (orderList != null && orderList.Any())
            {
                try
                {
                    var oCodes = new List<string>();
                    orderList.ForEach(item => oCodes.Add(item.vchOrderCode));
                    var orderStatusList = GetOrderAllStatusByOrderCodes(oCodes);
                    orderList.ForEach(item =>
                    {
                        var orderAllStatus = orderStatusList.Find(o => o.OrderCode == item.vchOrderCode);
                        if (orderAllStatus != null && !string.IsNullOrEmpty(orderAllStatus.OrderCode))
                        {
                            item.intPayState = MCvHelper.To(orderAllStatus.PayStatus, 0);
                            item.intOrderState = MCvHelper.To(orderAllStatus.OrderStatus, 1);
                        }
                    });
                }
                catch (Exception)
                {

                }
            }
        }

        /// <summary>
        /// 更新订单所有状态
        /// </summary>
        /// <param name="orderInfo"></param>
        public void UpdateOrderAllStatus(ref Sale_Order orderInfo)
        {
            try
            {
                var orderList = new List<Sale_Order> { orderInfo };
                UpdateOrderAllStatus(ref orderList);
            }
            catch (Exception)
            {
            }
        }

    }
}
