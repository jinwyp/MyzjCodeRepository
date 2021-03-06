﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Reflection;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using Core.Caching;
using Wcf.BLL.Goods;
using Wcf.Entity.Enum;
using Wcf.Entity.Goods;
using Factory;
using Core.ConfigUtility;
using Core.DataType;
using Core.DataTypeUtility;
using Core.LogUtility;
using Core.Enums;
using System.Web;

namespace Wcf.ServiceLibrary.Goods
{
    /// <summary>
    /// 
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [JavascriptCallbackBehavior(UrlParameterName = CommonUri.JAVASCRIPT_CALLBACKNAME)]
    public class GoodsService : BaseWcfService, IGoodsService
    {
        public MResultList<ItemGoods> GetGoodsList(string sid, string token, string guid, string user_id, string uid, string bid, string cid, string age, string price, string sort, string page, string size)
        {
            var result = new MResultList<ItemGoods>(true);

            try
            {
                var brandId = MCvHelper.To<int>(bid);
                var categoryId = MCvHelper.To<int>(cid);
                var pIndex = MCvHelper.To<int>(page);
                var pSize = MCvHelper.To<int>(size);
                var channelId = MCvHelper.To<SystemType>(sid);

                result = MCacheManager.UseCached<MResultList<ItemGoods>>(
                        string.Format("GetGoodsList_{0}_{1}_{2}_{3}_{4}_{5}_{6}_{7}_{8}", sid, user_id, bid, cid, age, price, sort, page, size),
                        MCaching.CacheGroup.Goods, () => GoodsBLL.GetGoodsList(sid, uid, (int)channelId, categoryId, brandId, age, price, sort, pSize, pIndex));
            }
            catch (Exception ex)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "处理数据出错！";
            }

            return result;
        }

        public MResult<ItemGoodsDetails> GetGoodsInfo(string sid, string token, string guid, string user_id, string uid, string gid)
        {
            var result = new MResult<ItemGoodsDetails>(true);

            try
            {
                var iGid = MCvHelper.To<int>(gid);
                result = GoodsBLL.GetGoodsInfo(base.SystemType.ToString(), base.Uid, (int)base.SystemType, iGid);
            }
            catch (Exception ex)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "处理数据出错！";
            }
            return result;
        }

        public MResultList<ProductImg> GetGoodsPicList(string sid, string token, string guid, string user_id, string uid, string gid)
        {
            var result = new MResultList<ProductImg>(true);

            try
            {
                var iGid = MCvHelper.To<int>(gid);
                result = MCacheManager.UseCached<MResultList<ProductImg>>(
                        string.Format("GetGoodsPicList{0}_{1}", sid, gid),
                        MCaching.CacheGroup.Goods,
                        () => GoodsBLL.GetGoodsPicList(sid, uid, iGid));
            }
            catch (Exception ex)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "处理图片列表数据出错！";
            }

            return result;
        }

        public MResult<List<ItemGoodsCategory>> GetGoodsCategoryList(string sid, string token, string guid, string user_id,
                                                              string uid)
        {
            var result = new MResult<List<ItemGoodsCategory>>();

            try
            {
                result = GoodsBLL.GetGoodsCategoryList(SystemType);
            }
            catch (Exception ex)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "处理数据出错！";
            }

            return result;
        }
    }
}
