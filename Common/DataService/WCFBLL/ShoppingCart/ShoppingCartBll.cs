using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Factory;
using Wcf.BLL.Goods;

using Core.DataType;

using Wcf.Entity.Order;
using Core.LogUtility;
using MemcachedProviders.Cache;
using Core.DataTypeUtility;
using Wcf.BLL;
using Core.Enums;
using Wcf.BLL.BaseData;

namespace Wcf.BLL.ShoppingCart
{
    /// <summary>
    /// 购物车相关业务类
    /// 作者： Zcc
    /// 时间： 2012-7-30
    /// </summary>
    public class ShoppingCartBll
    {
        /// <summary>
        /// 将商品加入购物车
        /// </summary>
        /// <param name="channelId">渠道ID</param>
        /// <param name="token">登录用户令牌</param>
        /// <param name="guid">未登录用户临时ID</param>
        /// <param name="user_id">登录用户ID</param>
        /// <param name="uid">登录用户，登录账户Email</param>
        /// <param name="area_id">区域ID</param>
        /// <param name="pid">商品ID</param>
        /// <param name="qty">商品数量</param>
        /// <returns></returns>
        public static MResult InsertShoppingCart(int channelId, string token, string guid, string user_id, string uid, string area_id, string pid, string qty)
        {
            #region 0会员等级
            //创建返回实体
            var result = new MResult();
            var userLevel = 0;
            try
            {
                if (!string.IsNullOrEmpty(uid) && !uid.Equals("null", StringComparison.CurrentCultureIgnoreCase))
                {
                    var member = Factory.DALFactory.Member();
                    var memberInfo = member.GetMemberInfo(uid);
                    userLevel = MCvHelper.To(memberInfo.clusterId, 0);
                }
                if (userLevel < 1)
                    userLevel = 1;

            }
            catch (Exception ex)
            {
                result.status = Core.Enums.MResultStatus.ExceptionError;
                result.msg = "查询用户数据错误！";
                return result;
            }
            #endregion

            #region 1、参数处理
            result.status = Core.Enums.MResultStatus.Success;

            //定义参数
            //int channelId = -1;
            int userId = -1;
            int productId = -1;
            int quantity = -1;

            //参数转换
            //int.TryParse(sid, out channelId);
            int.TryParse(pid.Trim(), out productId);
            int.TryParse(user_id, out userId);
            int.TryParse(qty, out quantity);

            #endregion

            #region 2、相关检测处理
            //商品数量检测
            if (quantity <= 0)
            {
                result.status = Core.Enums.MResultStatus.ParamsError;
                result.msg = "传入的商品数量参数有误！";
            }
            //商品ID转换
            if (productId <= 0)
            {
                result.status = Core.Enums.MResultStatus.ParamsError;
                result.msg = "传入的商品ID参数有误！";
            }
            //如果参数错误中断程序执行
            if (result.status == Core.Enums.MResultStatus.ParamsError)
                return result;

            #endregion

            #region 3、获取购物车中商品信息
            //获取购物车中信息
            var shoppingCartList = GetShoppingCartProductInfosByUserIDGuidChannelID(userId, guid, channelId);
            if (shoppingCartList != null && shoppingCartList.info.shoppingcart_list.Count > 0)
            {
                //获取当前商品的购物车中存在的商品信息
                var shoppingCartProductInfo = shoppingCartList.info.shoppingcart_list.Find(p => p.intProductID == productId);

                //检测购物车中是否存在当前商品
                if (shoppingCartProductInfo != null)
                {
                    //获取当前用户购物车中当前商品的数量
                    int productCount = (int)shoppingCartProductInfo.intBuyCount;

                    //检查是否是删除商品，合并数量或删除购物车中的删除商品
                    if (shoppingCartProductInfo.intIsDelete == 1)
                    {
                        DeleteShoppingCartByProductIdUserID(shoppingCartProductInfo.intShopCartID);
                    }
                    else
                    {
                        //如果不是删除状态，则将当前要购买的数量与购物车中的数量累加
                        quantity += productCount;
                        var shoppingCart = Factory.DALFactory.ShoppingCartDal();
                        if (shoppingCart.SetShoppingCartGoodsNum(userId, guid, shoppingCartProductInfo.intShopCartID, quantity))
                        {
                            result.status = Core.Enums.MResultStatus.Success;
                            result.msg = "添加购物车成功";
                            return result;
                        }
                        else
                        {
                            result.status = Core.Enums.MResultStatus.ExecutionError;
                            result.msg = "购物车更新商品数量失败！";
                            return result;
                        }
                    }
                }
            }

            #endregion

            #region 4、商品信息处理


            //检测商品实时库存
            if (!Wcf.BLL.Goods.GoodsBLL.CheckProductStockByProductID(productId, quantity))
            {
                result.status = Core.Enums.MResultStatus.ExecutionError;
                result.msg = "商品库存不足！";
                return result;
            }

            //获取商品信息
            var goodsDal = DALFactory.Goods();
            var pdt = goodsDal.GetGoodsInfo(userLevel, channelId, productId);

            //检查商品信息
            if (pdt == null)
            {
                result.status = Core.Enums.MResultStatus.ExecutionError;
                result.msg = "商品信息有误！";
                return result;
            }

            #endregion

            #region 5、处理商品加入购物车
            //给实体赋值
            ShoppingCartEntity spc = new ShoppingCartEntity();

            //购物车保护
            if (pdt.intProductID <= 0 || pdt.numVipPrice <= 0)
            {
                if (pdt.intAttribute != 5)
                {
                    result.msg = "商品加入购物车失败！";
                    result.status = Core.Enums.MResultStatus.ExecutionError;
                    return result;
                }
            }
            spc.vchGuid = spc.intUserID > 0 ? "" : guid;
            spc.intProductID = productId;
            spc.nchProductName = pdt.vchProductName;
            spc.intBuyCount = quantity;
            spc.intUserID = userId;
            spc.intBrandID = pdt.intBrandID;
            spc.intCateId = pdt.intWebChildType;
            spc.intChannelID = channelId;
            spc.chrIsGift = 0;
            spc.numOrgPrice = (decimal)pdt.numVipPrice;
            spc.intScore = (int)pdt.intScore;
            spc.numSalePrice = (decimal)pdt.numVipPrice;
            spc.intOrgScore = (int)pdt.intScore;
            spc.vchPicURL = pdt.vchMainPicURL;
            spc.intWeight = pdt.intWeight;
            spc.vchProductPrinted = pdt.vchProductPrinted;
            spc.dtAddTime = System.DateTime.Now;
            spc.numCost = (decimal)pdt.numCost;
            spc.numCleanCost = (decimal)pdt.numCleanCost;
            spc.intPromID = 0;
            spc.intSuitID = -1;
            spc.nchSuitName = "";
            spc.intPromCount = 1;
            spc.intGiftType = 0;
            spc.intAreaID = Convert.ToInt32(area_id);
            spc.vchproductcode = pdt.vchproductcode;
            spc.numMarketPrice = pdt.numMarketPrice;



            //加入购物车
            try
            {
                var retValue = AddShoppingCartProductInfo(spc);
                if (retValue)
                {
                    result.status = Core.Enums.MResultStatus.Success;
                    result.msg = "添加购物车成功";
                }
                else
                {
                    result.status = Core.Enums.MResultStatus.ExecutionError;
                    result.msg = "添加购物车失败！";
                }
            }
            catch (Exception ex)
            {
                result.status = Core.Enums.MResultStatus.ExceptionError;
                result.msg = "购物车出错了！";
            }
            #endregion

            return result;
        }

        /// <summary>
        /// 获取购物车所有商品信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="guid">用户全局变量</param>
        /// <param name="channelId">渠道ID</param>
        /// <returns></returns>
        public static MResult<ShoppingCartResult> GetShoppingCartProductInfosByUserIDGuidChannelID(int userId, string guid, int channelId)
        {
            var result = new MResult<ShoppingCartResult>(true);
            var shoppingCart = Factory.DALFactory.ShoppingCartDal();
            result.info.shoppingcart_list = shoppingCart.GetShoppingCartProductInfosByUserIDGuidChannelID(userId, guid, channelId);
            result.info.shoppingcart_total = result.info.shoppingcart_list.Sum(item => item.intBuyCount * item.numSalePrice);
            result.status = Core.Enums.MResultStatus.Success;
            return result;
        }

        /// <summary>
        /// 获取购物车商品列表 筛选 没有删除的商品，和 图片路径已经被格式化
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="guid">用户全局变量</param>
        /// <param name="channelId">渠道ID</param>
        /// <returns></returns>
        public static MResult<ShoppingCartResult> GetShoppingCartGoodsList(int userId, string guid, int channelId)
        {
            var result = new MResult<ShoppingCartResult>(true);
            var shoppingCart = Factory.DALFactory.ShoppingCartDal();
            var goodsList = shoppingCart.GetShoppingCartProductInfosByUserIDGuidChannelID(userId, guid, channelId);
            if (goodsList.Any())
            {
                result.info.shoppingcart_list = goodsList.FindAll(item => item.intIsDelete == 0);
                result.info.shoppingcart_list.ForEach(item =>
                {
                    item.vchPicURL = GoodsBLL.FormatProductPicUrl(item.vchPicURL);
                });
                result.info.shoppingcart_total = result.info.shoppingcart_list.Sum(item => item.intBuyCount * item.numSalePrice);
            }
            result.status = Core.Enums.MResultStatus.Success;
            return result;
        }

        /// <summary>
        /// 设置购物车商品数量
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="guid"></param>
        /// <param name="shoppingCartId"></param>
        /// <param name="gid"> </param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static MResult SetShoppingCartGoodsNum(int userId, string guid, int shoppingCartId, int gid, int num)
        {
            var result = new MResult();

            try
            {
                var shopping = DALFactory.ShoppingCartDal();
                var goods = DALFactory.Goods();
                if (goods.CheckProductStockByProductID(gid, num))
                {
                    result.status = shopping.SetShoppingCartGoodsNum(userId, guid, shoppingCartId, num)
                                 ? MResultStatus.Success
                                 : MResultStatus.ExecutionError;
                }
                else
                {
                    result.status = MResultStatus.LogicError;
                    result.msg = "该商品库存已不足！";
                }
            }
            catch (Exception)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "设置购物车商品数量 出错！";
            }

            return result;
        }

        /// <summary>
        /// 删除购物车中的商品信息
        /// </summary>
        /// <param name="shoppingCartId"></param>
        /// <returns></returns>
        public static MResult DeleteShoppingCartByProductIdUserID(int shoppingCartId)
        {
            var shoppingCart = Factory.DALFactory.ShoppingCartDal();
            return new MResult { status = shoppingCart.DeleteShoppingCartByProductIdUserID(shoppingCartId) ? MResultStatus.Success : MResultStatus.ExecutionError };
        }

        /// <summary>
        /// 添加购物车中的商品信息
        /// </summary>
        /// <param name="shoppingCartEntity"></param>
        /// <returns></returns>
        private static bool AddShoppingCartProductInfo(ShoppingCartEntity shoppingCartEntity)
        {
            var shoppingCart = Factory.DALFactory.ShoppingCartDal();
            return shoppingCart.AddShoppingCartProductInfo(shoppingCartEntity);
        }

        /// <summary>
        /// 获取购物车商品信息
        /// </summary>
        /// <param name="token"> </param>
        /// <param name="channelId"></param>
        /// <param name="uid"></param>
        /// <param name="user_id"> </param>
        /// <returns></returns>
        public static MResult<ItemShoppingCartGoodsSmall> GetShoppingCartGoodsNum(string guid, int channelId, string uid, int user_id)
        {
            var result = new MResult<ItemShoppingCartGoodsSmall>();
            try
            {
                if (!string.IsNullOrWhiteSpace(guid) || user_id > 0)
                {
                    var shopping = DALFactory.ShoppingCartDal();
                    result.info = new ItemShoppingCartGoodsSmall
                    {
                        goods_count = shopping.GetShoppingCartGoodsNum(guid, channelId, uid, user_id),
                        goods_total = shopping.GetShoppingTotal(user_id, guid, channelId)
                    };
                    result.status = Core.Enums.MResultStatus.Success;
                }

                else
                {
                    result.status = Core.Enums.MResultStatus.ParamsError;
                    result.msg = "参数错误！";
                }
            }
            catch (Exception)
            {
                result.status = Core.Enums.MResultStatus.ExecutionError;
                result.msg = "获取购物车商品信息 出错！";
            }
            return result;
        }

        /// <summary>
        /// 合并购物车商品
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="channelId"></param>
        /// <param name="uid"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static bool MergeShoppingCartGoods(string guid, int channelId, string uid, int userID)
        {
            var result = false;
            var mergeSuccessGids = new List<int>();

            var guidList = GetShoppingCartProductInfosByUserIDGuidChannelID(0, guid, channelId);
            var userIdList = GetShoppingCartProductInfosByUserIDGuidChannelID(userID, "", channelId);
            var shoppingcart = DALFactory.ShoppingCartDal();

            if (guidList != null && guidList.info.shoppingcart_list.Any())
            {
                var userIdGiList = new List<int>();
                userIdList.info.shoppingcart_list.ForEach(item => userIdGiList.Add(item.intProductID));

                guidList.info.shoppingcart_list.ForEach(item =>
                                                        {
                                                            try
                                                            {
                                                                var insertResult = InsertShoppingCart(MCvHelper.To<int>(channelId),
                                                                                                    "",
                                                                                                    item.vchGuid,
                                                                                                    userID + "",
                                                                                                    uid,
                                                                                                    item.intAreaID + "",
                                                                                                    item.intProductID + "",
                                                                                                    item.intBuyCount + "");

                                                                if (insertResult.status == MResultStatus.Success)
                                                                {
                                                                    //删除已经合并的 购物车商品
                                                                    if (shoppingcart.DeleteShoppingCartByProductIdUserID(item.intShopCartID))
                                                                        mergeSuccessGids.Add(item.intProductID);
                                                                }
                                                            }
                                                            catch
                                                            {
                                                            }

                                                        });
            }
            if (mergeSuccessGids.Count > 0)
                result = true;

            return result;
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="channelId"></param>
        /// <param name="uid"></param>
        /// <param name="userId"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static MResult<ItemOrder> CreateOrder(string guid, int channelId, string uid, int userId, OrderEntity order)
        {
            var result = new MResult<ItemOrder>();
            var memberDal = DALFactory.Member();
            var shoppingCart = Factory.DALFactory.ShoppingCartDal();

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
            if (order.addressid <= 0)
            {
                result.msg = "请选择收货地址！";
                result.status = MResultStatus.ParamsError;
                return result;
            }
            #endregion

            #region 支付方式
            if (order.payid <= 0)
            {
                result.msg = "请选择支付方式！";
                result.status = MResultStatus.ParamsError;
                return result;
            }
            #endregion

            #region 配送方式
            if (order.logisticsid <= 0)
            {
                result.msg = "请选择配送方式！";
                result.status = MResultStatus.ParamsError;
                return result;
            }
            #endregion

            #region 发票信息是否完整
            if (order.titletype == null)
            {
                result.msg = "请选择发票类型！";
                result.status = MResultStatus.ParamsError;
                return result;
            }
            if (order.invoicecategory == null)
            {
                result.msg = "请选择发票分类！";
                result.status = MResultStatus.ParamsError;
                return result;
            }
            if (order.titletype != Entity.Enum.Invoice.TitleType.NoNeed)
            {
                if (string.IsNullOrEmpty(order.invoicetitle))
                {
                    result.msg = "请填写发票抬头！";
                    result.status = MResultStatus.ParamsError;
                    return result;
                }
            }
            #endregion

            #endregion

            //购物车商品数据
            List<ShoppingCartEntity> norMalShoppingCartList = null;

            #region 判断购物车是否有商品
            var shoppingCartList = shoppingCart.GetShoppingCartProductInfosByUserIDGuidChannelID(userId, guid, channelId);
            if (shoppingCartList == null || shoppingCartList.Any())
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
            var addressInfo = memberDal.GetMemberAddressInfo(order.addressid);

            #region 验证收货地址
            #region 是否存在
            if (addressInfo == null || addressInfo.intAddressID <= 0 || addressInfo.intCityID <= 0 && order.payid <= 0 && order.logisticsid <= 0)
            {
                result.msg = "收货地址信息不正确！";
                result.status = MResultStatus.ParamsError;
                return result;
            }
            #endregion
            if (addressInfo.intCityID != 21)
            {
                
            }
            #endregion

            var summaryOrderInfo = SummaryOrderInfo(norMalShoppingCartList, channelId, userId, order.payid, order.logisticsid,
                                                 MCvHelper.To<int>(addressInfo.intCityID));


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
                    if (addressInfo != null && addressInfo.intCityID > 0 && order.payid > 0 && order.logisticsid > 0)
                    {
                        //查询该用户购物车所有商品
                        var shoppingCartList = shoppingCartDal.GetShoppingCartProductInfosByUserIDGuidChannelID(userId, guid, channelId);
                        if (shoppingCartList.Any())
                        {
                            //排除 已删除的商品
                            var notDelShoppingCart = (from a in shoppingCartList where a.intIsDelete == 0 select a).ToList();
                            if (notDelShoppingCart.Any())
                            {
                                var summaryOrderInfo = SummaryOrderInfo(notDelShoppingCart, channelId, userId, order.payid, order.logisticsid,
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
                result.status = Core.Enums.MResultStatus.ExecutionError;
                result.msg = "获取临时订单信息 出错！";
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
            catch
            {

            }
            return result;
        }

    }
}
