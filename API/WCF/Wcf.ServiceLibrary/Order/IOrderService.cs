using System.ServiceModel;
using Wcf.Entity.Order;
using Core.DataType;
using System.ServiceModel.Web;
using System.IO;

namespace Wcf.ServiceLibrary.Order
{
    [ServiceContract]
    public interface IOrderService
    {
        /// <summary>
        /// 添加商品到购物车
        /// </summary>
        /// <param name="token"> </param>
        /// <param name="uid"> </param>
        /// <param name="sid"></param>
        /// <param name="guid"> </param>
        /// <param name="area_id"> </param>
        /// <param name="gid"> </param>
        /// <param name="num"> </param>
        /// <returns></returns>
        [WebInvoke(Method = "GET", UriTemplate = OrderUri.ADDGOODSTOSHOPPINGCAR)]
        [OperationContract]
        MResult AddGoodsToShoppingCart(string sid, string token, string guid, string user_id, string uid, string area_id, string gid, string num);

        /// <summary>
        /// 删除购物车数据
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="token"></param>
        /// <param name="user_id"> </param>
        /// <param name="uid"></param>
        /// <param name="shoppingcarid">购物车ID</param>
        /// <param name="guid"> </param>
        /// <returns></returns>
        [WebInvoke(Method = "GET", UriTemplate = OrderUri.DELSHOPPINGCARTBYSCID)]
        [OperationContract]
        MResult RemoveShoppingCartByScId(string sid, string token, string guid, string user_id, string uid, string shoppingcarid);

        /// <summary>
        /// 设置购物车商品数量
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="token"></param>
        /// <param name="user_id"> </param>
        /// <param name="uid"></param>
        /// <param name="shoppingcarid">购物车ID</param>
        /// <param name="gid">商品ID</param>
        /// <param name="num">商品数量</param>
        /// <param name="guid"> </param>
        /// <returns></returns>
        [WebInvoke(Method = "GET", UriTemplate = OrderUri.SETSHOPPINGCARTGOODSNUM)]
        [OperationContract]
        MResult SetShoppingCartGoodsNum(string sid, string token, string guid, string user_id, string uid, string shoppingcarid, string gid, string num);

        /// <summary>
        /// 获取购物车商品列表
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="token"></param>
        /// <param name="uid"></param>
        /// <param name="guid"> </param>
        /// <param name="user_id"></param>
        /// <returns></returns>
        [WebGet(UriTemplate = OrderUri.GETSHOPPINGCARTGOODSLIST)]
        [OperationContract]
        MResult<ShoppingCartResult> GetShoppingCartGoodsList(string sid, string token, string guid, string user_id, string uid);

        /// <summary>
        /// 获取购物车商品数量
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="token"></param>
        /// <param name="uid"></param>
        /// <param name="guid"> </param>
        /// <param name="user_id"></param>
        /// <returns></returns>
        [WebGet(UriTemplate = OrderUri.GETSHOPPINGCARTGOODSNUM)]
        [OperationContract]
        MResult<ItemShoppingCartGoodsSmall> GetShoppingCartGoodsNum(string sid, string token, string guid, string user_id, string uid);

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="token"> </param>
        /// <param name="user_id"> </param>
        /// <param name="uid"> </param>
        /// <param name="order">订单实体</param>
        /// <param name="sid"></param>
        /// <param name="guid"> </param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", UriTemplate = OrderUri.CREATEORDER)]
        [OperationContract]
        MResult<ItemOrder> CreateOrder(string sid, string token, string guid, string user_id, string uid, OrderEntity order);

        /// <summary>
        /// 获取临时订单信息
        /// </summary>
        /// <param name="token"> </param>
        /// <param name="uid"> </param>
        /// <param name="order">订单实体</param>
        /// <param name="sid"></param>
        /// <param name="guid"> </param>
        /// <param name="user_id"> </param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", UriTemplate = OrderUri.GETTEMPORDERINFO)]
        [OperationContract]
        MResult<OrderResult> GetTempOrderInfo(string sid, string token, string guid, string user_id, string uid, OrderEntity order);

        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="token"></param>
        /// <param name="user_id"> </param>
        /// <param name="uid"> </param>
        /// <param name="orderCode">订单ID</param>
        /// <param name="guid"> </param>
        /// <returns></returns>
        [WebInvoke(Method = "GET", UriTemplate = OrderUri.GETORDERSINFO)]
        [OperationContract]
        MResult<ItemOrderDetails> GetOrderInfo(string sid, string token, string guid, string user_id, string uid, string orderCode);

        /// <summary>
        /// 获取订单商品列表
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="token"></param>
        /// <param name="guid"></param>
        /// <param name="user_id"></param>
        /// <param name="uid"></param>
        /// <param name="orderCode"></param>
        /// <returns></returns>
        [WebInvoke(Method = "GET", UriTemplate = OrderUri.GETORDERGOODSLIST)]
        [OperationContract]
        MResultList<ItemOrderGoods> GetOrderGoodsList(string sid, string token, string guid, string user_id, string uid,
                                                      string orderCode);

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="token"></param>
        /// <param name="guid"></param>
        /// <param name="user_id"></param>
        /// <param name="uid"></param>
        /// <param name="begintime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        [WebInvoke(Method = "GET", UriTemplate = OrderUri.GETORDERSLIST)]
        [OperationContract]
        MResultList<ItemOrder> GetOrdersList(string sid, string token, string guid, string user_id, string uid, string begintime, string endtime);

        /// <summary>
        /// 设置订单支付成功
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="token"></param>
        /// <param name="guid"></param>
        /// <param name="user_id"></param>
        /// <param name="uid"></param>
        /// <param name="getdata"> </param>
        /// <param name="postdata"> </param>
        /// <returns></returns>
        [WebGet(UriTemplate = OrderUri.ORDERPAYMENTSUCCESS)]
        [OperationContract]
        MResult OrderPaymentSuccess(string sid, string token, string guid, string user_id, string uid, string getdata, string postdata);

    }
}
