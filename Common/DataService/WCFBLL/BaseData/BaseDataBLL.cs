using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Factory;
using Wcf.Entity.BaseData;
using EF.Model.DataContext;
using Core.DataType;

using Core.Enums;
using System.Web;
using Core.DataTypeUtility;
using Core.Caching;
using Wcf.Entity.Enum;

namespace Wcf.BLL.BaseData
{
    /// <summary>
    /// 
    /// </summary>
    public static class BaseDataBLL
    {
        /// <summary>
        /// 获取支付方式
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="regionId"> </param>
        /// <returns></returns>
        public static MResultList<ItemPay> GetPaymentList(int channelId, int regionId)
        {
            var result = new MResultList<ItemPay>(true);

            try
            {
                var basedata = DALFactory.BaseData();
                result.list = basedata.GetPaymentList(channelId, regionId);
                result.status = MResultStatus.Success;
            }
            catch (Exception ex)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "获取支付方式列表异常！";
            }

            return result;
        }

        /// <summary>
        /// 获取配送方式
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="regionId"></param>
        /// <param name="payGroupId"></param>
        /// <returns></returns>
        public static MResultList<ItemLogistics> GetDeliverList(int channelId, int regionId, int payGroupId)
        {
            var result = new MResultList<ItemLogistics>(true);

            try
            {
                var basedata = DALFactory.BaseData();
                var list = basedata.GetDeliverList(channelId, regionId, payGroupId);
                list.ForEach(item =>
                                 {
                                     try
                                     {
                                         result.list.Add(new ItemLogistics
                                         {
                                             id = item.intDeliverID,
                                             name = item.vchDeliverName,
                                             remark = item.vchDeliverDesc
                                         });
                                     }
                                     catch
                                     {
                                     }
                                 });
                result.status = MResultStatus.Success;
            }
            catch (Exception ex)
            {
                result.status = MResultStatus.ExecutionError;
                result.msg = "获取配送方式列表异常！";
            }

            return result;
        }

        /// <summary>
        /// 获取运费总额
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="userId"> </param>
        /// <param name="cityId"></param>
        /// <param name="payId"></param>
        /// <param name="deliverId"> </param>
        /// <param name="totalWeight"></param>
        /// <param name="orderTotal"> </param>
        /// <returns></returns>
        public static MResult<decimal> GetLogisticsInfo(int channelId, int userId, int cityId, int payId, int deliverId, long totalWeight, decimal orderTotal)
        {
            var result = new MResult<decimal>(true);

            try
            {
                var baseDataDal = DALFactory.BaseData();
                var deliveryInfo = baseDataDal.GetDeliverInfo(deliverId);

                if (MCvHelper.To<byte>(deliveryInfo.intIsCarriage, 0) == 1)
                {
                    //上门自提，先不收运费
                    if (deliverId == 8)
                    {
                        result.info = 0;
                    }
                    else
                    {
                        //否则根据城市id 计算运费
                        result.info = baseDataDal.GetCarriage(totalWeight, deliverId, cityId);

                        #region  中通满100元江浙免运费
                        /*
                        if (orderTotal >= 100 && deliverId == 3)
                        {
                            List<City> clist = CityArea.GetCities(10);
                            clist.AddRange(CityArea.GetCities(11));

                            if (clist.Any(c => c.RegionId == cityId))
                            {
                                result.info = 0;
                            }
                        }
                        */
                        #endregion
                    }
                }
                else
                {
                    var memberDal = DALFactory.Member();
                    var memberInfo = memberDal.GetMemberInfo(userId.ToString());

                    //选择不需要运费的送货方式则50元起送，不足50元则加5元运费
                    if (deliverId == 8)
                    {
                        result.info = 0;
                    }
                    if (orderTotal < 50 && deliverId == 1)
                    {
                        result.info = 5;
                    }

                    if (orderTotal < 100 && deliverId == 6)
                    {
                        result.info = 5;
                    }

                    //月亮太阳会员不收运费
                    if (memberInfo != null
                        && (memberInfo.clusterId == 3 || memberInfo.clusterId == 4 || memberInfo.clusterId == 17 || memberInfo.clusterId == 18))
                    {
                        result.info = 0;
                    }
                }

            }
            catch (Exception ex)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "获取运费总额 错误！";
            }

            return result;
        }

        /// <summary>
        /// 获取区域列表数据
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public static MResultList<ItemRegion> GetRegionList(int parentId)
        {
            var result = new MResultList<ItemRegion>(true);
            try
            {
                var basedata = DALFactory.BaseData();
                var list = basedata.GetRegionList(parentId);
                list.ForEach(item =>
                                 {
                                     try
                                     {
                                         var regionType = string.Empty;
                                         switch (item.intRegionType)
                                         {
                                             case 1:
                                                 regionType = "国家"; break;
                                             case 2:
                                                 regionType = "省/直辖市"; break;
                                             case 3:
                                                 regionType = "地区"; break;
                                             case 4:
                                                 regionType = "区县"; break;
                                             default:
                                                 regionType = item.intRegionType + "";
                                                 break;
                                         }
                                         result.list.Add(new ItemRegion()
                                                             {
                                                                 id = item.intRegionID,
                                                                 name = item.vchRegionName,
                                                                 zip = item.vchPostCode,
                                                                 code = item.vchShortSpell,
                                                                 pid = item.intFRegionID,
                                                                 type = regionType
                                                             });
                                     }
                                     catch
                                     {
                                     }
                                 });
                result.status = MResultStatus.Success;
            }
            catch (Exception)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "获取区域列表数据异常";
            }
            return result;
        }

        /// <summary>
        /// 获取全部区域列表数据
        /// </summary>
        /// <returns></returns>
        public static MResult<List<ItemRegion>[]> GetAllRegionList()
        {
            var result = new MResult<List<ItemRegion>[]>();
            try
            {

                var provinceList = new List<ItemRegion>();
                var cityList = new List<ItemRegion>();
                var countyList = new List<ItemRegion>();

                var basedata = DALFactory.BaseData();
                var list = basedata.GetAllRegionList(0);

                #region 处理地区数据 转换
                list.ForEach(item =>
                {
                    try
                    {
                        var regionType = string.Empty;
                        switch (item.intRegionType)
                        {
                            case 1:
                                regionType = "国家";
                                break;
                            case 2:
                                regionType = "省/直辖市";
                                provinceList.Add(new ItemRegion()
                                {
                                    id = item.intRegionID,
                                    name = item.vchRegionName,
                                    zip = item.vchPostCode,
                                    code = item.vchShortSpell,
                                    pid = item.intFRegionID,
                                    type = regionType
                                });
                                break;
                            case 3:
                                regionType = "地区";
                                cityList.Add(new ItemRegion()
                                {
                                    id = item.intRegionID,
                                    name = item.vchRegionName,
                                    zip = item.vchPostCode,
                                    code = item.vchShortSpell,
                                    pid = item.intFRegionID,
                                    type = regionType
                                });
                                break;
                            case 4:
                                regionType = "区县";
                                countyList.Add(new ItemRegion()
                                {
                                    id = item.intRegionID,
                                    name = item.vchRegionName,
                                    zip = item.vchPostCode,
                                    code = item.vchShortSpell,
                                    pid = item.intFRegionID,
                                    type = regionType
                                });
                                break;
                            default:
                                regionType = item.intRegionType + "";
                                break;
                        }
                    }
                    catch
                    {
                    }
                });
                #endregion

                result.info = new[]
                        {
                            provinceList,
                            cityList,
                            countyList
                        };

                result.status = MResultStatus.Success;
                return result;

            }
            catch (Exception)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "获取区域列表数据异常";
            }
            return result;
        }

        /// <summary>
        /// 获取支付列表
        /// </summary>
        /// <param name="sType"></param>
        /// <param name="paygroupId"></param>
        /// <returns></returns>
        public static MResultList<ItemPay> GetPayList(SystemType sType, int paygroupId)
        {
            var result = new MResultList<ItemPay>(true);
            try
            {
                var baseDataDal = DALFactory.BaseData();
                List<Base_Pay_Type> payList = baseDataDal.GetPayList(sType, paygroupId);
                if (payList != null && payList.Any())
                {
                    payList.ForEach(item =>
                                        {
                                            try
                                            {
                                                var payItem = new ItemPay
                                                                {
                                                                    payid = item.intPayID,
                                                                    payname = item.vchPayName,
                                                                    paytype = item.intPayGroup,
                                                                    icon = string.IsNullOrWhiteSpace(item.vchPicAddr) ? "" : "http://muyingzhijia.com/" + item.vchPicAddr,
                                                                    payurl = item.vchPayURL,
                                                                    remark = item.vchPayDesc
                                                                };
                                                result.list.Add(payItem);
                                            }
                                            catch
                                            {
                                            }
                                        });
                }

                if (paygroupId == 0)
                    result.data = "货到付款（送货上门后再付款，支持现金或POS机刷卡)";
                else if (paygroupId == 1)
                    result.data = "在线支付（支持绝大数银行借记卡及部分银行信息卡）";
                else if (paygroupId == 3)
                    result.data = "支付宝（手机）支付（支持支付宝）";

                result.status = MResultStatus.Success;
            }
            catch (Exception)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "获取支付列表 异常";
            }
            return result;
        }
    }
}
