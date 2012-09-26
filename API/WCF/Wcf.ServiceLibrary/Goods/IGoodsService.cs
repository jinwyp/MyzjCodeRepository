using System.ServiceModel;
using Wcf.Entity.Enum;
using Core.DataType;
using Wcf.Entity.Goods;
using System.ServiceModel.Web;
using System.Collections.Generic;

namespace Wcf.ServiceLibrary.Goods
{
    [ServiceContract]
    public interface IGoodsService
    {
        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="sid">系统ID</param>
        /// <param name="token">用户令牌</param>
        /// <param name="uid">用户ID</param>
        /// <param name="key">搜索 关键字</param>
        /// <param name="bid">品牌ID</param>
        /// <param name="cid">分类ID</param>
        /// <param name="price">价格区间 10-20</param>
        /// <param name="sort">排序 类型</param>
        /// <param name="page">分页索引</param>
        /// <param name="size">每页大小</param>
        /// <param name="age">年龄区间 1-3</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = GoodsUri.GETGOODSLIST)]
        MResultList<ItemGoods> GetGoodsList(string sid, string token, string guid, string user_id, string uid, string key, string bid, string cid, string age, string price, string sort, string page, string size);

        /// <summary>
        /// 获取商品详细信息
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="token"></param>
        /// <param name="user_id"> </param>
        /// <param name="uid"> </param>
        /// <param name="gid">商品ID</param>
        /// <param name="guid"> </param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = GoodsUri.GETGOODSINFO)]
        MResult<ItemGoodsDetails> GetGoodsInfo(string sid, string token, string guid, string user_id, string uid, string gid);

        /// <summary>
        /// 获取商品图片列表
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="token"></param>
        /// <param name="uid"></param>
        /// <param name="gid"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = GoodsUri.GETGOODSPICLIST)]
        MResultList<ProductImg> GetGoodsPicList(string sid, string token, string guid, string user_id, string uid, string gid);

        /// <summary>
        /// 获取商品分类列表
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="token"></param>
        /// <param name="guid"></param>
        /// <param name="user_id"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = GoodsUri.GETGOODSCATEGORYLIST)]
        MResult<List<ItemGoodsCategory>> GetGoodsCategoryList(string sid, string token, string guid, string user_id,
                                                              string uid);

    }
}
