using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AopAlliance.Intercept;
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

namespace Wcf.SpringDotNetAdvice
{
    public class MethodInterceptor : IMethodInterceptor
    {
        public object Invoke(IMethodInvocation invocation)
        {
            var shopWatch = new Stopwatch();
            object result = null;
            var EnableMethodLog = MConfigManager.GetAppSettingsValue<bool>("EnableMethodLog", false);
            try
            {
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

                    var methodName = invocation.Method.Name;
                    var methodInfo = MCacheManager.GetCacheObj().GetValByKey<ItemMethodVerify>(methodName, MCaching.CacheGroup.System);
                    if (methodInfo != null)
                    {
                        var isVerifySystemId = methodInfo.IsVerifySystemId;
                        var isVerifyToKen = methodInfo.IsVerifyToken;
                        var isVerifyPermissions = methodInfo.IsVerfiyPemissions;

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

                        if (secureAuth.Verify().status != MResultStatus.Success)
                        {
                            var resultType = invocation.Method.ReturnType;
                            return Activator.CreateInstance(resultType);
                        }
                        result = invocation.Method.Invoke(invocation.This, args);
                        //result = invocation.Proceed();
                    }
                    else
                    {
                        result = invocation.Method.Invoke(invocation.This, args);
                    }
                }

                if (result == null)
                {
                    var resultType = invocation.Method.ReturnType;
                    return Activator.CreateInstance(resultType);
                }
                return result;
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.AopAdvice.方法拦截, "", "", ex);
            }
            finally
            {
                if (EnableMethodLog)
                {
                    shopWatch.Stop();
                    MLogManager.Info(MLogGroup.AopAdvice.方法拦截, "", "执行用时：{0}毫秒;请求信息：{1};返回信息：{2};",
                        shopWatch.ElapsedMilliseconds,
                        invocation.ToString(),
                        JsonConvert.SerializeObject(result));
                }
            }
            return result;
        }
    }
}
