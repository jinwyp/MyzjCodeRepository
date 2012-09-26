using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wcf.Entity.Enum;
using EF.Model.DataContext;
using Core.DataType;
using System.Data.Objects;

namespace EF.DAL
{
    public partial class Goods
    {
        /// <summary>
        /// 获取商品列表数据
        /// </summary>
        /// <param name="clusterId">群Id</param>
        /// <param name="channelId">渠道id</param>
        /// <param name="categoryId">分类id</param>
        /// <param name="key">关键字</param>
        /// <param name="brandId">品牌id</param>
        /// <param name="starAge">开始年龄</param>
        /// <param name="endAge">结束年龄</param>
        /// <param name="startPrice">开始价格</param>
        /// <param name="endPrice">结束价格</param>
        /// <param name="sortType">排序类型</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageTotal">数据总数</param>
        /// <returns></returns>
        public List<Vi_Web_Pdt_List> GetGoodsList(int clusterId, int channelId, int categoryId, string key, int brandId,
                                                    int starAge, int endAge, decimal startPrice, decimal endPrice,
                                                    SortType? sortType, int pageSize, int pageIndex,
                                                    out int pageTotal)
        {
            channelId = 102;
            var holycaDb = new HolycaEntities();
            if (sortType == null)
                sortType = SortType.SalesDesc;

            var queryTxt = from c in holycaDb.Vi_Web_Pdt_List
                           where c.intHerdID == clusterId && c.intChannelID == channelId
                           select c;

            #region 处理查询条件

            if (categoryId > 0)
                queryTxt = queryTxt.Where(p => p.intWebType == categoryId || p.intWebChildType == categoryId || p.intWebThirdType == categoryId);

            if (brandId > 0)
                queryTxt = queryTxt.Where(p => p.intBrandID == brandId);

            if (starAge > 0)
                queryTxt = queryTxt.Where(p => p.intStartAge <= starAge);

            if (endAge > 0)
                queryTxt = queryTxt.Where(p => p.intEndAge >= endAge);

            if (startPrice > 0)
                queryTxt = queryTxt.Where(p => p.numHerdPrice >= startPrice);

            if (endPrice > 0)
                queryTxt = queryTxt.Where(p => p.numHerdPrice <= endPrice);

            if (!string.IsNullOrWhiteSpace(key))
                queryTxt = queryTxt.Where(p => p.vchProductName.Contains(key));

            switch (sortType)
            {
                case SortType.EnableTimeAsc:
                    queryTxt = queryTxt.OrderBy(o => o.intIsNew);
                    break;
                case SortType.EnableTimeDesc:
                    queryTxt = queryTxt.OrderByDescending(o => o.intIsNew);
                    break;
                case SortType.PriceAsc:
                    queryTxt = queryTxt.OrderBy(o => o.numHerdPrice);
                    break;
                case SortType.PriceDesc:
                    queryTxt = queryTxt.OrderByDescending(o => o.numHerdPrice);
                    break;
                case SortType.SalesAsc:
                    queryTxt = queryTxt.OrderBy(o => o.intSalesVolume);
                    break;
                case SortType.SalesDesc:
                    queryTxt = queryTxt.OrderByDescending(o => o.intSalesVolume);
                    break;
            }

            #endregion

            pageTotal = queryTxt.Count();
            var list = queryTxt.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();

            return list;
        }

        /// <summary>
        /// 获取专题关联的商品列表 
        /// </summary>
        /// <param name="clusterId"></param>
        /// <param name="channelId"></param>
        /// <param name="columnCode"></param>
        /// <param name="columnId"></param>
        /// <returns></returns>
        public List<Vi_Web_Pdt_List> GetGoodsListBySubject(int clusterId, int channelId, string columnCode, int columnId, int page, int size, out int pageTotal)
        {
            var holycaDb = new HolycaEntities();
            channelId = 102;
            var queryTxt = from c in holycaDb.Vi_Web_Pdt_List
                           join a in holycaDb.Web_Subject_Section_Pdt on c.intProductID equals a.intProductID
                           join b in holycaDb.Web_ContentData on a.intSubjectID equals b.Wcd_ResId
                           where c.intHerdID == clusterId && c.intChannelID == channelId
                           && b.Wcd_ResType == 4 && b.Wcd_Code == columnCode && b.Wcd_Id == columnId
                           orderby a.intSort ascending
                           select c;

            pageTotal = queryTxt.Count();
            return queryTxt.Skip(size * (page - 1)).Take(size).ToList();
        }

    }
}
