using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Caching;
using Core.Enums;
using Wcf.Entity.BaseData;

namespace Wcf.ServiceLibrary
{
    /// <summary>
    /// Wcf 授权验证
    /// </summary>
    public static class WcfAuthManage
    {
        /// <summary>
        /// 初始化 权限验证列表
        /// </summary>
        public static void InitPermissionsVerifyList()
        {
            
        }

        /// <summary>
        /// 初始化 方法验证列表
        /// </summary>
        public static void InitMethodVerifyList()
        {
            var methodDict = new Dictionary<string, ItemMethodVerify>();
            methodDict.Add("GetGoodsInfo", new ItemMethodVerify { IsVerifySystemId = true });
            methodDict.Add("GetGoodsList", new ItemMethodVerify { IsVerifySystemId = true });
            methodDict.Add("LoginMember", new ItemMethodVerify { IsVerifySystemId = true });

            foreach (var methodInfo in methodDict)
            {
                MCacheManager.GetCacheObj().Set<ItemMethodVerify>(methodInfo.Key, MCaching.CacheGroup.Pemissions, methodInfo.Value);
            }

        }
    }
}
