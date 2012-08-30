using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DataType;
using Core.Enums;
using Newtonsoft.Json;
using Wcf.Entity.Enum;
using System.Reflection;
using EF.Model.DataContext;
using System.ServiceModel.Web;
using Factory;

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
                                                                       ReturnParameters = methodInfo.ReturnType.FullName,
                                                                   };
                        permissionEntity.RefreshTime = DateTime.Now;

                        #region 解析方法特性
                        var methodAttrData = methodInfo.GetCustomAttributesData();

                        var requestType = string.Empty;
                        var requestUri = string.Empty;
                        foreach (var customAttributeData in methodAttrData)
                        {
                            if (customAttributeData.NamedArguments != null)
                                foreach (var customAttributeNamedArgument in customAttributeData.NamedArguments)
                                {
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
                                                   addCount++;
                                           }
                                           else
                                           {
                                               item.RefreshTime = DateTime.Now;
                                               if (manageDal.AddSystemPermission(item))
                                                   updateCount++;
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
    }
}
