using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DataType;
using Core.Enums;
using Newtonsoft.Json;
using Wcf.Entity.BaseData;
using Wcf.Entity.Enum;
using System.Reflection;
using EF.Model.DataContext;
using System.ServiceModel.Web;
using Factory;
using Wcf.Entity.Manage;
using Core.DataTypeUtility;
using Core.Caching;

namespace Wcf.BLL.Manage
{
    /// <summary>
    /// 系统管理 业务
    /// </summary>
    public static class ManageBLL
    {
        /// <summary>
        /// 刷新 授权数据
        /// </summary>
        /// <param name="systemType"></param>
        /// <param name="token"></param>
        /// <param name="guid"></param>
        /// <param name="uid"></param>
        /// <param name="privatekey"></param>
        /// <returns></returns>
        public static MResult RefreshAuthData(SystemType systemType, string token, string guid, string uid, string privatekey)
        {
            var result = new MResult();

            var addCount = 0;
            var updateCount = 0;

            try
            {
                var wcfApiList = new List<System_Permission>();

                var assembly = Assembly.Load("Wcf.ServiceLibrary");
                var types = assembly.GetTypes();
                //程序集的类遍历
                foreach (var type in types)
                {
                    //遍历 所有接口，因为特性附加在 接口上
                    if (!type.IsInterface) continue;

                    var methods = type.GetMethods();
                    //类的方法遍历
                    foreach (var methodInfo in methods)
                    {
                        #region 实体赋值
                        var permissionEntity = new System_Permission
                                                                   {
                                                                       MethodName = methodInfo.Name,
                                                                       ClassName = type.Name,
                                                                       AssemblyName = assembly.GetName().Name,
                                                                       IsVerifySystemId = 1,
                                                                       IsEnableCache = 0,
                                                                       IsVerifyData = 0,
                                                                       IsVerifyToken = 0
                                                                   };
                        permissionEntity.RefreshTime = DateTime.Now;
                        permissionEntity.ReturnParameters = methodInfo.ReturnType.FullName;

                        #region 解析方法特性
                        var methodAttrData = methodInfo.GetCustomAttributesData();

                        var requestType = string.Empty;
                        var requestUri = string.Empty;
                        foreach (var customAttributeData in methodAttrData)
                        {
                            if (customAttributeData.NamedArguments != null)
                                foreach (var customAttributeNamedArgument in customAttributeData.NamedArguments)
                                {
                                    if (customAttributeNamedArgument.MemberInfo.ReflectedType == typeof(WebGetAttribute))
                                        requestType = "GET";

                                    switch (customAttributeNamedArgument.MemberInfo.Name)
                                    {
                                        case "Method":
                                            {
                                                if (customAttributeNamedArgument.MemberInfo.ReflectedType == typeof(WebGetAttribute))
                                                    requestType = "GET";
                                                else if (customAttributeNamedArgument.MemberInfo.ReflectedType == typeof(WebInvokeAttribute))
                                                    requestType = customAttributeNamedArgument.TypedValue.Value.ToString().ToUpper();
                                                break;
                                            }
                                        case "UriTemplate":
                                            {
                                                requestUri = customAttributeNamedArgument.TypedValue.Value.ToString();
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                }
                        }
                        #endregion
                        permissionEntity.RequestType = requestType;
                        permissionEntity.RequestUri = requestUri;
                        permissionEntity.MethodAttrs = JsonConvert.SerializeObject(methodAttrData);

                        var methodParameters = methodInfo.GetParameters();
                        permissionEntity.AfferentParameters =
                            string.Join(",", methodParameters.Select(methodParameter => methodParameter.Name).ToArray());
                        #endregion

                        wcfApiList.Add(permissionEntity);
                    }
                }

                var manageDal = DALFactory.Manage();

                wcfApiList.ForEach(item =>
                                       {
                                           var id = manageDal.CheckIsExistsSystemPermission(item);
                                           if (id > 0)
                                           {
                                               item.Id = id;
                                               if (manageDal.UpdateSystemPermission(item))
                                                   updateCount++;
                                           }
                                           else
                                           {
                                               item.Created = DateTime.Now;
                                               if (manageDal.AddSystemPermission(item))
                                                   addCount++;
                                           }
                                       });

                result.status = MResultStatus.Success;
                result.msg = string.Format("新增：{0}条记录，更新：{1}条记录！", addCount, updateCount);
            }
            catch (Exception)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "刷新授权 数据出错！";
            }

            return result;
        }

        /// <summary>
        /// 获取 系统权限列表
        /// </summary>
        /// <returns></returns>
        public static List<ItemMethodVerify> GetSystemPermissionList()
        {
            var result = new List<ItemMethodVerify>();
            var manageDal = DALFactory.Manage();
            var permissionList = manageDal.GetSystemPermissionList();
            if (permissionList.Any())
            {
                permissionList.ForEach(item =>
                                           {
                                               try
                                               {
                                                   result.Add(new ItemMethodVerify
                                                                  {
                                                                      MethodName = item.MethodName,
                                                                      IsVerfiyPemissions = MCvHelper.To(item.IsVerfiyPemissions, false),
                                                                      IsVerifyData = MCvHelper.To(item.IsVerifyData, false),
                                                                      IsVerifySystemId = MCvHelper.To(item.IsVerifySystemId, false),
                                                                      IsVerifyToken = MCvHelper.To(item.IsVerifyToken, false),
                                                                      IsEnableCache = MCvHelper.To(item.IsEnableCache, false)
                                                                  });
                                               }
                                               catch
                                               {
                                               }
                                           });
            }
            return result;
        }

        /// <summary>
        /// 刷新缓存组版本
        /// </summary>
        /// <param name="sType"></param>
        /// <param name="userId"></param>
        /// <param name="uid"></param>
        /// <param name="cacheGroup"></param>
        /// <returns></returns>
        public static MResult RefreshCacheGroupVersion(SystemType sType, string userId, string uid, MCaching.CacheGroup cacheGroup)
        {
            var result = new MResult();
            try
            {
                var version = MCacheManager.RefreshCacheGroupVersion(cacheGroup);
                if (!string.IsNullOrEmpty(version))
                {
                    result.status = MResultStatus.Success;
                    result.msg = cacheGroup + "版本更新为：" + version;
                    result.data = version;
                }
                else
                {
                    result.status = MResultStatus.ParamsError;
                    result.msg = "刷新缓存版本失败！";
                }
            }
            catch (Exception)
            {
                result.status = MResultStatus.ExceptionError;
                result.msg = "执行刷新缓存组版本异常";
            }
            return result;
        }
    }
}
