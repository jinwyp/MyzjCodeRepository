using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Caching;
using Core.Enums;
using Wcf.Entity.BaseData;
using Factory;
using Wcf.BLL.Manage;
using Wcf.Entity.Manage;

namespace Wcf.ServiceLibrary
{
    /// <summary>
    /// Wcf 授权验证
    /// </summary>
    public static class WcfAuthManage
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            var methodPermisstionList = ManageBLL.GetSystemPermissionList();
            if (methodPermisstionList.Any())
                MCacheManager.GetCacheObj().Set("SystemPermission",
                    MCaching.CacheGroup.Pemissions, methodPermisstionList);
        }

    }
}
