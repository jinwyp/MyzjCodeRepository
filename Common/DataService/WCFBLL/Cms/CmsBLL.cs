using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DataType;
using Core.Enums;
using Wcf.Entity.Cms;
using Wcf.Entity.Enum;
using Factory;
using Core.DataTypeUtility;
using Wcf.BLL.Goods;
using Wcf.Entity.Goods;
using Newtonsoft.Json;

namespace Wcf.BLL.Cms
{
    /// <summary>
    /// 内容管理 业务逻辑
    /// </summary>
    public static class CmsBLL
    {
        /// <summary>
        /// 获取栏位数据列表
        /// </summary>
        /// <param name="sType"></param>
        /// <param name="user_id"></param>
        /// <param name="uid"></param>
        /// <param name="columncode"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static MResultList<ColumnData> GetColumnDataList(SystemType sType, string user_id, string uid, string columncode, int page, int size)
        {
            var result = new MResultList<ColumnData>(true);
            try
            {
                var cmsDal = DALFactory.Cms();
                var total = 0;
                var columnDataList = cmsDal.GetColumnDataList(columncode, page, size, out total);
                if (columnDataList != null && columnDataList.Any())
                {
                    columnDataList.ForEach(item =>
                                               {
                                                   try
                                                   {
                                                       #region 实体赋值
                                                       var columnItem = new ColumnData
                                                                        {
                                                                            id = item.Wcd_Id,
                                                                            content = item.Wcd_Content,
                                                                            link = item.Wcd_Link,
                                                                            pic_url = GoodsBLL.FormatProductPicUrl(item.Wcd_Image),
                                                                            resid = item.Wcd_ResId,
                                                                            restype = item.Wcd_ResType,
                                                                            title = item.Wcd_Title,
                                                                            f1 = item.Wcd_F1,
                                                                            f2 = item.Wcd_F2,
                                                                            f3 = item.Wcd_F3,
                                                                            f4 = item.Wcd_F4,
                                                                            f5 = item.Wcd_F5,
                                                                            f6 = item.Wcd_F6,
                                                                            f7 = item.Wcd_F7,
                                                                            f8 = item.Wcd_F8,
                                                                            f9 = item.Wcd_F9
                                                                        };
                                                       #endregion
                                                       result.list.Add(columnItem);
                                                   }
                                                   catch
                                                   {
                                                   }
                                               });
                    result.page = page;
                    result.size = size;
                    result.total = total;
                }
                result.status = MResultStatus.Success;
            }
            catch (Exception)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "";
            }
            return result;
        }

        /// <summary>
        /// 获取公告列表
        /// </summary>
        /// <param name="sType"></param>
        /// <param name="user_id"></param>
        /// <param name="uid"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static MResultList<ItemNotice> GetNoticeList(SystemType sType, string user_id, string uid, int page, int size)
        {
            var result = new MResultList<ItemNotice>(true);
            try
            {
                var cmsDal = DALFactory.Cms();
                var total = 0;
                var noticeList = cmsDal.GetNoticeList(page, size, out total);
                if (noticeList != null && noticeList.Any())
                {
                    noticeList.ForEach(item =>
                    {
                        try
                        {
                            #region 实体赋值
                            var columnItem = new ItemNotice
                            {
                                id = item.intBulletinID,
                                title = item.vchBulletinName,
                                //content = item.vchBulletinContent,
                                created = item.dtAddDate
                            };
                            #endregion
                            result.list.Add(columnItem);
                        }
                        catch
                        {
                        }
                    });
                    result.page = page;
                    result.size = size;
                    result.total = total;
                }
                result.status = MResultStatus.Success;
            }
            catch (Exception)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "";
            }
            return result;
        }

        /// <summary>
        /// 获取公告详细
        /// </summary>
        /// <param name="sType"></param>
        /// <param name="noticeid"></param>
        /// <returns></returns>
        public static MResult<ItemNotice> GetNoticeInfo(SystemType sType, int noticeid)
        {
            var result = new MResult<ItemNotice>();
            try
            {
                var cmsDal = DALFactory.Cms();
                var noticeInfo = cmsDal.GetNoticeInfo(noticeid);
                if (noticeInfo != null && noticeInfo.intBulletinID > 0)
                {
                    #region 实体赋值
                    result.info = new ItemNotice
                    {
                        id = noticeInfo.intBulletinID,
                        title = noticeInfo.vchBulletinName,
                        content = noticeInfo.vchBulletinContent,
                        created = noticeInfo.dtAddDate
                    };
                    #endregion
                }
                result.status = MResultStatus.Success;
            }
            catch (Exception)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "获取公告详细异常";
            }
            return result;
        }

        /// <summary>
        /// 获取栏位数据信息
        /// </summary>
        /// <param name="systemType"></param>
        /// <param name="userId"></param>
        /// <param name="uid"></param>
        /// <param name="columncode"></param>
        /// <param name="columnId"></param>
        /// <param name="page"> </param>
        /// <param name="size"> </param>
        /// <returns></returns>
        public static MResultList<object> GetColumnDataInfo(SystemType systemType, string userId, string uid, string columncode, int columnId, int page, int size)
        {
            var result = new MResultList<object>();

            try
            {
                var cmsDal = DALFactory.Cms();
                var columnData = cmsDal.GetColumnDataInfo(columncode, columnId);
                if (columnData != null && columnData.Wcd_Id > 0)
                {
                    /*
                        文章 = 1,
                        图片 = 2,
                        商品 = 3,
                        专题 = 4
                     */

                    switch (columnData.Wcd_ResType)
                    {
                        case 4:
                            {
                                var clusterId = 1;
                                #region 会员等级
                                if (!string.IsNullOrEmpty(uid))
                                {
                                    var member = Factory.DALFactory.Member();
                                    var memberInfo = member.GetMemberInfo(uid);
                                    if (memberInfo != null)
                                        clusterId = MCvHelper.To(memberInfo.clusterId, 0);
                                }
                                #endregion

                                var pageTotal = 0;
                                var goodsDal = DALFactory.Goods();
                                var list = goodsDal.GetGoodsListBySubject(clusterId, (int)systemType, columncode, columnId, page, size, out pageTotal);
                                var resultList = new List<ItemGoods>();
                                list.ForEach(item =>
                                {
                                    try
                                    {
                                        resultList.Add(new ItemGoods()
                                        {
                                            gid = item.intProductID,
                                            title = item.vchProductName,
                                            pic_url = GoodsBLL.FormatProductPicUrl(item.vchMainPicURL),
                                            price = MCvHelper.To<decimal>(item.numVipPrice, 0)
                                        });
                                    }
                                    catch
                                    {
                                    }
                                });
                                result.total = pageTotal;
                                result.page = page;
                                result.size = size;
                                result.data = JsonConvert.SerializeObject(resultList);
                                result.status = MResultStatus.Success;
                            }
                            break;
                    }
                }
                else
                {
                    result.status = MResultStatus.ParamsError;
                    result.msg = "没有找到该栏位数据！";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("获取栏位数据信息", ex);
            }

            return result;
        }
    }
}
