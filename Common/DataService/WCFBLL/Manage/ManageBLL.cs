using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DataType;
using Core.Enums;
using Wcf.Entity.Enum;
using System.Reflection;
using EF.Model.DataContext;

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

            try
            {
                var wcfApiList = new List<System_Permission>();

                var assembly = Assembly.Load("Wcf.ServiceLibrary");
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    var methods = type.GetMethods();
                    foreach (var methodInfo in methods)
                    {
                        wcfApiList.Add(
                        new System_Permission
                        {
                            MethodName = methodInfo.Name,
                            ClassName = type.Name,
                            AssemblyName = assembly.FullName,
                            Created = DateTime.Now,
                            ReturnParameters = methodInfo.ReturnType.FullName
                        });
                    }
                }
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
