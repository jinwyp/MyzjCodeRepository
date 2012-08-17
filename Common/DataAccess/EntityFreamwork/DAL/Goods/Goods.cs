using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EF.Model.DataContext;
using EF.Model.DataContext;
using EF.Model.DataContext;

namespace EF.DAL
{
    public partial class Goods
    {
        /// <summary>
        /// 获取商品信息
        /// </summary>
        /// <param name="userLevel"></param>
        /// <param name="channelId"></param>
        /// <param name="gid"></param>
        /// <returns></returns>
        public Vi_Web_Pdt_Detail GetGoodsInfo(int userLevel, int channelId, int gid)
        {
            var holycaDb = new HolycaEntities();

            var queryTxt = from a in holycaDb.Vi_Web_Pdt_Detail
                           where a.intProductID == gid && a.intChannelID == channelId && a.intHerdID == userLevel
                           select a;
            return queryTxt.FirstOrDefault();
        }

        /// <summary>
        /// 获取商品其他信息
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public Pdt_Other_Info GetGoodsOtherInfo(int gid)
        {
            var holycaDb = new HolycaEntities();

            var queryTxt = from c in holycaDb.Pdt_Other_Info
                           where c.intProductID == gid
                           select c;
            return queryTxt.FirstOrDefault();
        }


        /// <summary>
        /// 检测商品库存
        /// </summary>
        /// <param name="productId">商品ID</param>
        /// <returns></returns>
        public bool CheckProductStockByProductID(int productId, int stockQuantity)
        {
            var holycaDb = new HolycaEntities();

            var queryTxt = from c in holycaDb.Pdt_Stock
                           where c.intProductID == productId
                           select c.intStockQty;

            return queryTxt.FirstOrDefault() >= stockQuantity;
        }

    }
}
