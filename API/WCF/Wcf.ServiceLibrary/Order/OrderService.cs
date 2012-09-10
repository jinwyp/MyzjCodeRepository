using System;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using Wcf.BLL.Order;
using Wcf.Entity.Order;
using Core.DataType;
using Core.DataTypeUtility;
using Core.Enums;
using Wcf.BLL.ShoppingCart;

namespace Wcf.ServiceLibrary.Order
{
    /// <summary>
    /// 
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [JavascriptCallbackBehavior(UrlParameterName = CommonUri.JAVASCRIPT_CALLBACKNAME)]
    public class OrderService : BaseWcfService, IOrderService
    {
        public MResult AddGoodsToShoppingCart(string sid, string token, string guid, string user_id, string uid, string area_id, string gid, string num)
        {
            var result = new MResult();
            try
            {
                result = ShoppingCartBll.InsertShoppingCart((int)SystemType, token, guid, user_id, uid, area_id, gid, num);
            }
            catch (Exception)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "处理数据出错！";
            }
            return result;
        }

        public MResult RemoveShoppingCartByScId(string sid, string token, string guid, string user_id, string uid, string shoppingcarid)
        {
            var result = new MResult();
            try
            {
                var shoppcartId = MCvHelper.To<int>(shoppingcarid);
                result = ShoppingCartBll.DeleteShoppingCartByProductIdUserID(shoppcartId);
            }
            catch (Exception)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "处理数据出错！";
            }
            return result;
        }

        public MResult SetShoppingCartGoodsNum(string sid, string token, string guid, string user_id, string uid, string shoppingcarid, string gid, string num)
        {
            var result = new MResult();
            try
            {
                var Gid = MCvHelper.To<int>(gid);
                var shoppcartId = MCvHelper.To<int>(shoppingcarid);
                var Num = MCvHelper.To<int>(num);
                result = ShoppingCartBll.SetShoppingCartGoodsNum(UserId, guid, shoppcartId, Gid, Num);
            }
            catch (Exception)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "处理数据出错！";
            }
            return result;
        }

        public MResult<ShoppingCartResult> GetShoppingCartGoodsList(string sid, string token, string guid, string user_id, string uid)
        {
            var result = new MResult<ShoppingCartResult>();
            try
            {
                result = ShoppingCartBll.GetShoppingCartGoodsList(UserId, guid, (int)SystemType);
            }
            catch (Exception)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "处理数据出错！";
            }
            return result;
        }

        public MResult<ItemShoppingCartGoodsSmall> GetShoppingCartGoodsNum(string sid, string token, string guid, string user_id, string uid)
        {
            var result = new MResult<ItemShoppingCartGoodsSmall>();

            try
            {
                result = ShoppingCartBll.GetShoppingCartGoodsNum(guid, (int)SystemType, Uid, base.UserId);
            }
            catch (Exception ex)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "处理数据出错！";
            }
            return result;
        }

        public MResult<ItemOrder> CreateOrder(string sid, string token, string guid, string user_id, string uid, OrderEntity order)
        {
            var result = new MResult<ItemOrder>();

            try
            {
                result = OrderBLL.CreateOrder(guid, (int)SystemType, Uid, UserId, order);
            }
            catch (Exception ex)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "处理数据出错！";
            }

            return result;
        }

        public MResult<OrderResult> GetTempOrderInfo(string sid, string token, string guid, string user_id, string uid, OrderEntity order)
        {
            var result = new MResult<OrderResult>();

            try
            {
                result = OrderBLL.GetTempOrderInfo(guid, (int)SystemType, Uid, UserId, order);
            }
            catch (Exception ex)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "处理数据出错！";
            }

            return result;
        }

        public MResult<ItemOrderDetails> GetOrderInfo(string sid, string token, string guid, string user_id, string uid, string orderCode)
        {
            var result = new MResult<ItemOrderDetails>();
            try
            {
                result = OrderBLL.GetOrderinfo((int)SystemType, Uid, UserId, orderCode);
            }
            catch (Exception ex)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "处理数据出错！";
            }

            return result;
        }

        public MResultList<ItemOrderGoods> GetOrderGoodsList(string sid, string token, string guid, string user_id, string uid,
                                                      string orderCode)
        {
            var result = new MResultList<ItemOrderGoods>();
            try
            {
                result = OrderBLL.GetOrderGoodsList((int)SystemType, Uid, UserId, orderCode);
            }
            catch (Exception ex)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "处理数据出错！";
            }

            return result;
        }

        public MResultList<ItemOrder> GetOrdersList(string sid, string token, string guid, string user_id, string uid, string begintime, string endtime)
        {
            var result = new MResultList<ItemOrder>();
            try
            {
                var begimTime = MCvHelper.To<DateTime>(begintime, DateTime.Now.AddMonths(-1));
                var endTime = MCvHelper.To<DateTime>(endtime, DateTime.Now);

                result = OrderBLL.GetOrdersList((int)SystemType, Uid, UserId, begimTime, endTime);
            }
            catch (Exception ex)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "处理数据出错！";
            }

            return result;
        }

        public MResult OrderPaymentSuccess(string sid, string token, string guid, string user_id, string uid, string getdata, string postdata)
        {
            var result = new MResult();
            try
            {
                result = OrderBLL.OrderPaymentSuccess(SystemType, UserId, Uid, getdata, postdata);
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
