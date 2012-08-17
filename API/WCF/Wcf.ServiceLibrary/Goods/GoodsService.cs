using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Reflection;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
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

                result = GoodsBLL.GetGoodsList(sid, uid, (int)channelId, categoryId, brandId, age, price, sort, pSize, pIndex);
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
                //TODO:log
            }
            return result;
        }

        public MResultList<ProductImg> GetGoodsPicList(string sid, string token, string guid, string user_id, string uid, string gid)
        {
            var result = new MResultList<ProductImg>(true);

            try
            {
                var iGid = MCvHelper.To<int>(gid);
                result = GoodsBLL.GetGoodsPicList(sid, uid, iGid);
            }
            catch (Exception)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "处理图片列表数据出错！";
            }

            return result;
        }

    }
}
