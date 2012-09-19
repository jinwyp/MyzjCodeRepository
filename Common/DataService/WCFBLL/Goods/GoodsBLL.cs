using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Factory;
using Wcf.Entity.Enum;
using Wcf.Entity.Goods;
using Core.ConfigUtility;
using Core.DataType;
using EF.Model.DataContext;
using Core.Enums;
using Core.LogUtility;
using MemcachedProviders.Cache;
using Core.DataTypeUtility;


namespace Wcf.BLL.Goods
{
    public static partial class GoodsBLL
    {
        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="sid"> </param>
        /// <param name="uid">用户id</param>
        /// <param name="channelId">调用 渠道</param>
        /// <param name="categoryId">分类id</param>
        /// <param name="brandId">品牌id</param>
        /// <param name="age">年龄区间</param>
        /// <param name="price">价格区间</param>
        /// <param name="sort">排序类型</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="pageIndex">当前页数</param>
        /// <returns></returns>
        public static MResultList<ItemGoods> GetGoodsList(string sid, string uid, int channelId, int categoryId, int brandId,
                                                    string age, string price, string sort, int pageSize, int pageIndex)
        {
            var result = new MResultList<ItemGoods>(true);

            var clusterId = 0;
            var starAge = 0;
            var endAge = 0;
            decimal startPrice = 0;
            decimal endPrice = 0;
            SortType? sortType = null;
            var pageTotal = 0;

            #region 数据转换
            try
            {
                #region 会员等级
                if (!string.IsNullOrEmpty(uid))
                {
                    var member = Factory.DALFactory.Member();
                    var memberInfo = member.GetMemberInfo(uid);
                    clusterId = MCvHelper.To(memberInfo.clusterId, 0);
                }
                if (clusterId < 1)
                    clusterId = 1;
                #endregion

                #region 年龄
                if (!string.IsNullOrEmpty(age))
                {
                    var ageSplit = age.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                    if (ageSplit.Length == 2)
                    {
                        starAge = MCvHelper.To<int>(ageSplit[0]);
                        endAge = MCvHelper.To<int>(ageSplit[1]);
                    }
                }
                #endregion

                #region 价格
                if (!string.IsNullOrEmpty(price))
                {
                    var priceSplit = price.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                    if (priceSplit.Length == 2)
                    {
                        startPrice = MCvHelper.To<decimal>(priceSplit[0]);
                        endPrice = MCvHelper.To<decimal>(priceSplit[1]);
                    }
                }
                #endregion

                #region 排序类型
                if (!string.IsNullOrEmpty(sort))
                {
                    sortType = MCvHelper.To<SortType>(sort);
                }
                #endregion

            }
            catch (Exception ex)
            {
                result.status = MResultStatus.ExecutionError;
                result.msg = "调用参数异常，请检查参数格式";
                MLogManager.Error(MLogGroup.Goods.读取商品数据, null, "调用参数异常，请检查参数格式", ex);
            }

            #endregion

            try
            {
                var goods = Factory.DALFactory.Goods();
                var list = goods.GetGoodsList(clusterId, channelId, categoryId, brandId, starAge, endAge, startPrice,
                    endPrice, sortType, pageSize, pageIndex, out pageTotal);
                result.page = pageIndex;
                result.size = pageSize;
                result.total = pageTotal;
                result.status = MResultStatus.Success;

                list.ForEach(item =>
                {
                    try
                    {
                        result.list.Add(new ItemGoods()
                        {
                            gid = item.intProductID,
                            title = item.vchProductName,
                            pic_url = FormatProductPicUrl(item.vchMainPicURL),
                            price = MCvHelper.To<decimal>(item.numVipPrice, 0)
                        });
                    }
                    catch
                    {
                    }
                });
            }
            catch (Exception ex)
            {
                result.status = MResultStatus.ExecutionError;
                MLogManager.Error(MLogGroup.Goods.读取商品数据, null, "调用读取商品列表出错", ex);
            }
            return result;
        }

        /// <summary>
        /// 格式化图片地址
        /// </summary>
        /// <param name="imgUrl"></param>
        /// <returns></returns>
        public static string FormatProductPicUrl(string imgUrl)
        {
            if(string.IsNullOrWhiteSpace(imgUrl)) return imgUrl;
            if (imgUrl.StartsWith("http://", StringComparison.CurrentCultureIgnoreCase)) return imgUrl;

            var picHost =
                MConfigManager.GetAppSettingsValue<string>(MConfigManager.FormatKey("Pic",
                                                                                    Core.Enums.MConfigs.
                                                                                        ConfigsCategory.Host));
            return string.IsNullOrWhiteSpace(picHost) ? imgUrl : picHost + "product/{0}/" + imgUrl.Trim('/');
        }

        /// <summary>
        /// 获取商品详细
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="uid"></param>
        /// <param name="channelId"></param>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static MResult<ItemGoodsDetails> GetGoodsInfo(string sid, string uid, int channelId, int gid)
        {
            var result = new MResult<ItemGoodsDetails>();

            #region 会员等级
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
                result.status = MResultStatus.ExecutionError;
                result.msg = "查询用户数据错误！";
            }
            #endregion


            var goods = DALFactory.Goods();
            try
            {
                var picHost =
                MConfigManager.GetAppSettingsValue<string>(MConfigManager.FormatKey("Pic",
                                                                                    Core.Enums.MConfigs.
                                                                                        ConfigsCategory.Host));

                var info = goods.GetGoodsInfo(userLevel, channelId, gid);
                if (info != null && info.intProductID > 0)
                {
                    var attrList = new List<KeyValuePair<string, object>>();
                    #region 处理其他属性

                    var goodsOtherInfo = GoodsBLL.GetGoodsOtherInfo(sid, uid, gid);
                    if (goodsOtherInfo.status == MResultStatus.Success)
                    {
                        if (!string.IsNullOrWhiteSpace(info.vchBrandName))
                            attrList.Add(new KeyValuePair<string, object>("品牌", info.vchBrandName));
                        if (!string.IsNullOrWhiteSpace(info.vchProColr))
                            attrList.Add(new KeyValuePair<string, object>("颜色", info.vchProColr));
                        if (!string.IsNullOrWhiteSpace(info.vchSpec))
                            attrList.Add(new KeyValuePair<string, object>("规格", info.vchSpec));
                        if (!string.IsNullOrWhiteSpace(goodsOtherInfo.info.vchFactory))
                            attrList.Add(new KeyValuePair<string, object>("产地", goodsOtherInfo.info.vchFactory));
                        if (!string.IsNullOrWhiteSpace(goodsOtherInfo.info.vchMaterial))
                            attrList.Add(new KeyValuePair<string, object>("材质", goodsOtherInfo.info.vchMaterial));
                        if (goodsOtherInfo.info.intSeason != null && goodsOtherInfo.info.intSeason > 0)
                        {
                            var seasonType = MCvHelper.To<SeasonType>(goodsOtherInfo.info.intSeason);
                            attrList.Add(new KeyValuePair<string, object>("季节性", seasonType.ToString()));
                        }
                        if (goodsOtherInfo.info.intPeriod != null && goodsOtherInfo.info.intPeriod > 0)
                            attrList.Add(new KeyValuePair<string, object>("保修期", goodsOtherInfo.info.intPeriod + "天"));

                        if (goodsOtherInfo.info.intShelfLift != null && goodsOtherInfo.info.intShelfLift > 0)
                            attrList.Add(new KeyValuePair<string, object>("保质期", goodsOtherInfo.info.intShelfLift + "月"));

                    }
                    #endregion

                    result.info = new ItemGoodsDetails()
                    {
                        gid = info.intProductID,
                        title = info.vchProductName,
                        productcode = info.vchProductPrinted,
                        desc = (HttpUtility.HtmlDecode(info.txtWebShowInfo).Replace("/Purchase/img/Description/", picHost + "product/Description/")),
                        marketprice = MCvHelper.To<decimal>(info.numMarketPrice, 0),
                        price = MCvHelper.To<decimal>(info.numVipPrice, 0),
                        pic_url = FormatProductPicUrl(info.vchMainPicURL),
                        score = info.intScore == null ? 0 : (int)info.intScore,
                        stock = MCvHelper.To<long>(info.intStockQty, 0),
                        attrs = attrList
                    };
                    result.status = MResultStatus.Success;
                }
            }
            catch (Exception ex)
            {
                result.status = MResultStatus.ExecutionError;
                result.msg = "查询商品数据错误！";
            }

            return result;
        }

        /// <summary>
        /// 获取商品图片列表
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="uid"></param>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static MResultList<ProductImg> GetGoodsPicList(string sid, string uid, int gid)
        {
            var result = new MResultList<ProductImg>(true);

            try
            {
                var goods = DALFactory.Goods();
                var list = goods.GetGoodsPicList(gid);
                list.ForEach(picInfo =>
                {
                    try
                    {
                        result.list.Add(new ProductImg
                        {
                            id = picInfo.intPicID,
                            product_id = picInfo.intProductID,
                            url = FormatProductPicUrl(picInfo.vchPicURL),
                            position = MCvHelper.To<int>(picInfo.vchPicType, 0),
                            created = picInfo.dtAddTime
                        });
                    }
                    catch
                    { }
                });
                result.status = MResultStatus.Success;
            }
            catch (Exception ex)
            {
                result.status = MResultStatus.ExecutionError;
                result.msg = "查询商品列表数据出错！";
            }

            return result;
        }

        /// <summary>
        /// 获取商品其他信息
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="uid"></param>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static MResult<Pdt_Other_Info> GetGoodsOtherInfo(string sid, string uid, int gid)
        {
            var result = new MResult<Pdt_Other_Info>();

            try
            {
                var goods = DALFactory.Goods();
                result.info = goods.GetGoodsOtherInfo(gid);

                result.status = MResultStatus.Success;
            }
            catch (Exception ex)
            {
                result.status = MResultStatus.ExecutionError;
                result.msg = "查询商品其他信息数据出错！";
            }

            return result;
        }

        /// <summary>
        /// 检测商品库存
        /// </summary>
        /// <param name="productId">商品ID</param>
        /// <param name="stockQuantity">商品数量</param>
        /// <returns></returns>
        public static bool CheckProductStockByProductID(int productId, int stockQuantity)
        {
            var goods = DALFactory.Goods();
            return goods.CheckProductStockByProductID(productId, stockQuantity);
        }

        /// <summary>
        ///  获取商品分类列表
        /// </summary>
        /// <param name="systemType"></param>
        /// <returns></returns>
        public static MResult<List<ItemGoodsCategory>> GetGoodsCategoryList(SystemType systemType)
        {
            var result = new MResult<List<ItemGoodsCategory>>();

            try
            {
                var goodsDal = DALFactory.Goods();
                var goodsCategoryList = goodsDal.GetGoodsCategoryList();
                result.info = RecursiveGoodsCategory(systemType, 0, goodsCategoryList);
                result.status = MResultStatus.Success;
            }
            catch (Exception)
            {
                result.status = MResultStatus.ExecutionError;
                result.msg = "获取商品分类列表 数据出错！";
            }

            return result;
        }

        /// <summary>
        /// 递归商品分类
        /// </summary>
        /// <param name="sType"> </param>
        /// <param name="pid"> </param>
        /// <param name="goodsCategoryList"></param>
        /// <returns></returns>
        public static List<ItemGoodsCategory> RecursiveGoodsCategory(SystemType sType, int pid, List<Web_Pdt_Type> goodsCategoryList)
        {
            var result = new List<ItemGoodsCategory>();
            if (goodsCategoryList.Any())
            {
                var level1List = goodsCategoryList.FindAll(item => item.intCateFather == pid);
                if (!level1List.Any()) return result;

                foreach (var level1Info in level1List)
                {
                    var level1Entity = new ItemGoodsCategory();
                    level1Entity.id = level1Info.intCateID;

                    #region 判断系统
                    if (sType == SystemType.MobileWebSite)
                        level1Entity.name = level1Info.vchCateName;
                    else if (sType == SystemType.WebSite)
                        level1Entity.name = level1Info.vchWebShowName;
                    else
                        level1Entity.name = level1Info.vchCateName;
                    #endregion

                    level1Entity.pid = MCvHelper.To<int>(level1Info.intCateFather, 0);

                    var level2List = goodsCategoryList.FindAll(item => item.intCateFather == level1Info.intCateID);
                    if (level2List.Any())
                        level1Entity.child = RecursiveGoodsCategory(sType, level1Info.intCateID, goodsCategoryList);

                    result.Add(level1Entity);
                }
            }
            return result;
        }

    }
}
