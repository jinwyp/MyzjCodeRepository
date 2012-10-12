using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wcf.Entity.BaseData;

using EF.Model.DataContext;
using Core.DataTypeUtility;
using System.Data;
using Wcf.Entity.Enum;

namespace EF.DAL
{
    public partial class BaseData
    {
        /// <summary>
        /// 获取支付方式
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="regionId"> </param>
        /// <returns>list</returns>
        public List<ItemPay> GetPaymentList(int channelId, int regionId)
        {
            var result = new List<ItemPay>();

            var holycaDb = new HolycaEntities();
            var queryTxt = from a in holycaDb.Base_Pay_Type
                           join b in holycaDb.Base_Deliver_Pay on a.intPayID equals b.intPayID
                           join c in holycaDb.Base_Deliver_Area on b.intDeliverID equals c.intDeliverID
                           where c.intRegionID == regionId  && a.intIsEnable==1
                           group a by a.intPayGroup into g
                           select g;

            var payList = queryTxt.ToList();

            payList.ForEach(item =>
                                {
                                    if (item.Key == 0)
                                        result.Add(new ItemPay { payid = 0, payname = "货到付款", paytype = 0, remark = "（送货上门后再付款，支持现金或POS机刷卡)" });
                                    else if (item.Key == 1)
                                    {
                                        if (channelId != (int)SystemType.MobileWebSite)
                                            result.Add(new ItemPay { payid = 1, payname = "在线支付", paytype = 1, remark = "（支持绝大数银行借记卡及部分银行信息卡）" });
                                    }
                                    else if (item.Key == 3)
                                    {
                                        if (channelId == (int)SystemType.MobileWebSite)
                                            result.Add(new ItemPay { payid = 3, payname = "支付宝（手机）支付", paytype = 3, remark = "（支持支付宝）" });
                                    }

                                });

            result.Sort((l1, l2) => l2.payid.CompareTo(l1.payid));

            return result;
        }

        /// <summary>
        /// 获取配送方式列
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="regionId"></param>
        /// <param name="payGroupId"></param>
        /// <returns></returns>
        public List<Base_Deliver> GetDeliverList(int channelId, int regionId, int payGroupId)
        {
            var holycaDb = new HolycaEntities();
            var queryTxt = from s in holycaDb.Base_Deliver
                           where (
                           from a in holycaDb.Base_Deliver_Pay
                           join b in holycaDb.Base_Deliver_Area on a.intDeliverID equals b.intDeliverID
                           join c in holycaDb.Base_Pay_Type on a.intPayID equals c.intPayID
                           where b.intIsEnable == 1 && b.intRegionID == regionId
                           && c.intIsEnable == 1 && c.intPayGroup == payGroupId
                           && s.intIsEnable==1
                           select a.intDeliverID).Contains(s.intDeliverID)
                           select s;
            var result = queryTxt.ToList();

            return result;
        }

        /// <summary>
        /// 获取配送方式
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <returns></returns>
        public Base_Deliver GetDeliverInfo(int deliveryId)
        {
            using (var db = new HolycaEntities())
            {
                var queryTxt = from a in db.Base_Deliver
                               where a.intDeliverID == deliveryId
                               select a;
                return queryTxt.FirstOrDefault();
            }
        }

        /// <summary>
        /// 计算运费
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="deliveId"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public decimal GetCarriage(long weight, int deliveId, int cityId)
        {
            using (var db = new HolycaEntities())
            {
                decimal result = 0;
                var objectParams = new System.Data.Objects.ObjectParameter("intcarriage", DbType.Int32);
                var carriage = db.Up_ShopCart_GetCarriage((int)weight, deliveId, cityId, objectParams);
                var upOutPutValue = MCvHelper.To<int>(objectParams.Value, 0);
                if (upOutPutValue > 0)
                    result = upOutPutValue / 100;
                return result;
            }
        }

        /// <summary>
        /// 获取地区数据
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<Base_Region> GetRegionList(int parentId)
        {
            if (parentId < 0)
                parentId = 0;
            var holycaDb = new HolycaEntities();
            var queryTxt = from a in holycaDb.Base_Region
                           where a.intFRegionID == parentId
                           orderby a.intRegionID ascending
                           select a;
            return queryTxt.ToList();
        }

        /// <summary>
        /// 获取全部地区数据
        /// </summary>
        /// <param name="regionType">地区类型</param>
        /// <returns></returns>
        public List<Base_Region> GetAllRegionList(int regionType)
        {
            using (var holycaDb = new HolycaEntities())
            {
                IQueryable<Base_Region> queryTxt = from a in holycaDb.Base_Region
                                                   orderby a.intRegionID ascending
                                                   select a;
                if (regionType > 0)
                    queryTxt = queryTxt.Where(c => c.intRegionType == regionType);
                return queryTxt.ToList();
            }
        }

        /// <summary>
        /// 获取支付列表
        /// </summary>
        /// <param name="sType"></param>
        /// <param name="paygroupId"></param>
        /// <returns></returns>
        public List<Base_Pay_Type> GetPayList(SystemType sType, int paygroupId)
        {
            using (var db = new HolycaEntities())
            {
                var queryTxt = from a in db.Base_Pay_Type
                               where a.intPayGroup == paygroupId && a.intIsEnable == 1
                               orderby a.intSortID descending
                               select a;
                return queryTxt.ToList();
            }
        }

        /// <summary>
        /// 获取支付信息
        /// </summary>
        /// <param name="payId"></param>
        /// <returns></returns>
        public Base_Pay_Type GetPaymentInfo(int payId)
        {
            using (var db = new HolycaEntities())
            {
                var queryTxt = from a in db.Base_Pay_Type
                               where a.intPayID == payId && a.intIsEnable == 1
                               select a;
                return queryTxt.FirstOrDefault();
            }
        }
    }
}
