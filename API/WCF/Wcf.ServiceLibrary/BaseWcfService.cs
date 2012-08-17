using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.ConfigUtility;
using Wcf.Entity.Enum;
using System.Web;
using Core.DataTypeUtility;
using Wcf.SpringDotNetAdvice.Validate;
using Core.Caching;
using Wcf.Entity.BaseData;
using Core.LogUtility;
using Core.Enums;

namespace Wcf.ServiceLibrary
{
    public class BaseWcfService
    {
        protected string Uid { get; set; }
        protected string Token { get; set; }
        protected string Guid { get; set; }
        protected int UserId { get; set; }
        protected SystemType SystemType { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseWcfService()
        {
            var httpContext = HttpContext.Current;
            if (httpContext != null)
            {
                var args = httpContext.Request.Url.Segments;
                for (var i = 0; i < args.Length; i++)
                    args[i] = ValidateUtility.CheckNull(args[i].Trim(new char[] { '/', '\\', ' ' })).ToString();
                if (args.Length >= 8)
                {
                    SystemType = MCvHelper.To<SystemType>(args[3]);
                    Token = MCvHelper.To<string>(args[4]);
                    Guid = MCvHelper.To<string>(args[5]);
                    UserId = MCvHelper.To<int>(args[6]);
                    Uid = MCvHelper.To<string>(args[7]);
                }
                else
                {
                    MLogManager.Error(MLogGroup.WcfService.构造函数, "", "wcf 服务基类 构造函数初始化 ，uri参数错误");
                }
            }
        }

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
                MCacheManager.GetCacheObj().Set<ItemMethodVerify>(methodInfo.Key, Core.Enums.MCaching.CacheGroup.System, methodInfo.Value);
            }

        }

    }
}
