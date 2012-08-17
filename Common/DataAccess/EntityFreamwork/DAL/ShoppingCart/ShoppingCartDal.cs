using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;

using EF.Model;
using EF.Model.DataContext;
using Wcf.Entity.Order;
using Core.DataTypeUtility;

namespace EF.DAL
{
    public class ShoppingCartDal
    {
        /// <summary>
        /// 获取当前用户的购物车中的商品信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="guid">用户全局变量</param>
        /// <param name="channelId">渠道ID</param>
        /// <returns></returns>
        public List<ShoppingCartEntity> GetShoppingCartProductInfosByUserIDGuidChannelID(int userId, string guid, int channelId)
        {
            var holycaDb = new HolycaEntities();
            List<ShoppingCartEntity> resultList = new List<ShoppingCartEntity>();
            ShoppingCartEntity shoppingCartEntity;
            List<Sale_ShoppingCart> retList = new List<Sale_ShoppingCart>();

            //用户登录了就用userId查询，用户未登录用Guid查
            if (userId > 0)
            {
                var queryTxt = from c in holycaDb.Sale_ShoppingCart
                               where c.intUserID == userId && c.intChannelID == channelId
                               select c;
                retList = queryTxt.ToList();
            }
            else
            {
                var queryTxt = from c in holycaDb.Sale_ShoppingCart
                               where c.vchGuid == guid && c.intChannelID == channelId
                               select c;

                retList = queryTxt.ToList();
            }
            if (retList != null && retList.Count > 0)
            {
                foreach (Sale_ShoppingCart ssc in retList)
                {
                    shoppingCartEntity = new ShoppingCartEntity();
                    MCvHelper.ObjectCopyTo(ssc, shoppingCartEntity);
                    resultList.Add(shoppingCartEntity);
                }
            }
            return resultList;
        }

        /// <summary>
        /// 删除购物车中的商品信息
        /// </summary>
        /// <param name="shoppingCartId">购物车ID</param>
        /// <returns></returns>
        public bool DeleteShoppingCartByProductIdUserID(int shoppingCartId)
        {
            var holycaDb = new HolycaEntities();

            Sale_ShoppingCart queryTxt = holycaDb.Sale_ShoppingCart.Where(c => c.intShopCartID == shoppingCartId).FirstOrDefault();

            holycaDb.DeleteObject(queryTxt);

            holycaDb.SaveChanges();

            return true;

        }

        /// <summary>
        /// 增加购物车商品信息
        /// </summary>
        /// <param name="shoppingCartEntity">购物车信息实体</param>
        /// <returns></returns>
        public bool AddShoppingCartProductInfo(ShoppingCartEntity shoppingCartEntity)
        {
            var holycaDb = new HolycaEntities();
            Sale_ShoppingCart sale_ShoppingCart = new Sale_ShoppingCart();
            MCvHelper.ObjectCopyTo(shoppingCartEntity, sale_ShoppingCart);
            try
            {
                holycaDb.Sale_ShoppingCart.AddObject(sale_ShoppingCart);
                holycaDb.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 设置购物车商品数量
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="guid"></param>
        /// <param name="shoppingCartId"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public bool SetShoppingCartGoodsNum(int user_id, string guid, int shoppingCartId, int num)
        {
            var result = false;
            if (num > 0)
            {
                using (var holycaDb = new HolycaEntities())
                {
                    var shoppingCartEntity = holycaDb.Sale_ShoppingCart.First(s => s.intShopCartID == shoppingCartId);
                    if (shoppingCartEntity != null)
                    {
                        try
                        {
                            shoppingCartEntity.intBuyCount = num;
                            holycaDb.SaveChanges();
                            result = true;
                        }
                        catch (Exception)
                        {
                            holycaDb.Refresh(RefreshMode.ClientWins, shoppingCartEntity);
                            holycaDb.SaveChanges();
                            result = false;
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取购物车总金额
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="guid"></param>
        /// <param name="channelId"> </param>
        /// <returns></returns>
        public decimal GetShoppingTotal(int user_id, string guid, int channelId)
        {
            decimal result = 0;
            using (var holycaDb = new HolycaEntities())
            {
                if (user_id > 0 || !string.IsNullOrEmpty(guid))
                {
                    var queryTxt = from a in holycaDb.Sale_ShoppingCart where a.intChannelID == channelId && a.intIsDelete == 0 select a;
                    if (user_id > 0)
                        queryTxt = queryTxt.Where(w => w.intUserID == user_id);
                    else if (!string.IsNullOrEmpty(guid))
                        queryTxt = queryTxt.Where(w => w.vchGuid == guid);
                    try
                    {
                        result = MCvHelper.To<decimal>(queryTxt.Sum(s => (decimal?)(s.intBuyCount * s.numSalePrice)), 0);
                    }
                    catch
                    {
                        result = -1;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取购物车商品数量
        /// </summary>
        /// <param name="token"> </param>
        /// <param name="channelId"></param>
        /// <param name="uid"></param>
        /// <param name="user_id"> </param>
        /// <returns></returns>
        public int GetShoppingCartGoodsNum(string token, int channelId, string uid, int user_id)
        {
            using (var holycaDb = new HolycaEntities())
            {
                var queryTxt = from c in holycaDb.Sale_ShoppingCart
                               where c.intChannelID == channelId
                                     && c.intIsDelete == 0
                               select c;
                if (user_id > 0)
                    queryTxt = queryTxt.Where(w => w.intUserID == user_id);
                else if (!string.IsNullOrEmpty(token))
                    queryTxt = queryTxt.Where(w => w.vchGuid == token);
                else return 0;
                try
                {
                    var result = queryTxt.Sum(c => (int?)c.intBuyCount);
                    return MCvHelper.To<int>(result, 0);
                }
                catch
                {
                    return -1;
                }
            }
        }

    }
}
