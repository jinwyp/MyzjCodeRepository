using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AopAlliance.Intercept;
using Wcf.Entity.Manage;
using Wcf.SpringDotNetAdvice.Validate;
using Core.DataTypeUtility;
using Core.LogUtility;
using Core.Enums;
using System.Diagnostics;
using Newtonsoft.Json;
using Wcf.Entity.Enum;
using Core.Caching;
using Wcf.Entity.BaseData;
using Core.DataType;
using Core.ConfigUtility;
using Spring.Web.Support;

namespace Wcf.SpringDotNetAdvice
{
    /// <summary>
    /// 拦截方法
    /// </summary>
    public class MethodInterceptor : IMethodInterceptor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invocation"></param>
        /// <returns></returns>
        public object Invoke(IMethodInvocation invocation)
        {
            var shopWatch = new Stopwatch();
            object result = null;
            var resultType = invocation.Method.ReturnType;
            var enableMethodLog = MConfigManager.GetAppSettingsValue<bool>("EnableMethodLog", false);

            shopWatch.Start();

            //{sid}/{token}/{guid}/{user_id}/{uid}
            var args = invocation.Arguments;
            for (var i = 0; i < args.Length; i++)
                args[i] = ValidateUtility.CheckNull(args[i]);
            if (args.Length >= 5)
            {
                var sid = MCvHelper.To<string>(args[0]);
                var token = MCvHelper.To<string>(args[1]);
                var guid = MCvHelper.To<string>(args[2]);
                var userID = MCvHelper.To<int>(args[3]);
                var uid = MCvHelper.To<string>(args[4]);

                try
                {
                    var methodName = invocation.Method.Name;
                    var methodCacheList = MCacheManager.GetCacheObj().GetValByKey<List<ItemMethodVerify>>("SystemPermission", MCaching.CacheGroup.Pemissions);

                    if (methodCacheList != null && methodCacheList.Any())
                    {
                        var methodCacheInfo =
                            methodCacheList.Find(
                                item => item.MethodName.Equals(methodName, StringComparison.InvariantCultureIgnoreCase));

                        if (methodCacheInfo != null)
                        {
                            var isVerifySystemId = methodCacheInfo.IsVerifySystemId;
                            var isVerifyToKen = methodCacheInfo.IsVerifyToken;
                            var isVerifyPermissions = methodCacheInfo.IsVerfiyPemissions;

                            #region 初始化 验证对象
                            var secureAuth = new SecureAuth
                                                                     {
                                                                         IsVerifySystemId = isVerifySystemId,
                                                                         IsVerifyToKen = isVerifyToKen,
                                                                         IsVerifyPermissions = isVerifyPermissions,
                                                                         Sid = sid,
                                                                         Token = token,
                                                                         Uid = uid,
                                                                         UserId = userID
                                                                     };
                            #endregion

                            #region 验证 权限 和 调用方法
                            if (secureAuth.Verify().status == MResultStatus.Success)
                            {
                                if (methodCacheInfo.IsEnableCache)
                                {
                                    var cacheKey = string.Format("{0}_{1}", methodName,
                                                                 string.Join("_", invocation.Arguments));

                                    result = MCacheManager.UseCached<object>(cacheKey, MCaching.CacheGroup.Pemissions,
                                                                             () =>
                                                                             invocation.Method.Invoke(invocation.This,
                                                                                                      args));
                                }
                                else
                                {
                                    result = invocation.Method.Invoke(invocation.This, args);
                                }
                            }
                            #endregion
                            //result = invocation.Proceed();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MLogManager.Error(MLogGroup.AopAdvice.方法拦截, sid, uid, "所有内部错误", ex);
                }
                finally
                {
                    if (enableMethodLog)
                    {
                        shopWatch.Stop();
                        MLogManager.Info(MLogGroup.AopAdvice.方法拦截, sid, uid, "执行用时：{0}毫秒;请求信息：{1};返回信息：{2};",
                            shopWatch.ElapsedMilliseconds,
                            invocation.ToString(),
                            JsonConvert.SerializeObject(result));
                    }
                }
            }

            if (result == null)
            {
                result = Activator.CreateInstance(resultType);

                var statusProperty = resultType.GetProperty("status");
                if (statusProperty != null)
                    statusProperty.SetValue(result, (int)MResultStatus.ExceptionError, null);

                var msgProperty = resultType.GetProperty("msg");
                if (msgProperty != null)
                    msgProperty.SetValue(result, "调用异常", null);

            }

            return result;

        }
    }
}
