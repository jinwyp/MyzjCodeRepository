using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;

namespace Core.FactoryUtility
{
    public class MFactoryManager
    {
        /// <summary>
        /// 被缓存的程序集方法
        /// </summary>
        private static Dictionary<string, object> _assemblyDict;

        /// <summary>
        /// 获取程序集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelName"></param>
        /// <param name="sectionPath">groupSectionName/sectionName</param>
        /// <param name="enableCache"></param>
        /// <returns></returns>
        public static T GetFactoryAssembly<T>(string modelName, string sectionPath, bool enableCache)
        {
            if (_assemblyDict == null)
                _assemblyDict = new Dictionary<string, object>();

            var dictKey = string.Format("{0}-{1}", modelName, sectionPath);
            try
            {
                object result = null;
                if (enableCache && _assemblyDict.ContainsKey(dictKey))
                {
                    result = _assemblyDict[dictKey];
                }
                else
                {
                    var assemblyName = string.Empty;

                    var assemblys = (NameValueCollection)ConfigurationManager.GetSection(sectionPath);
                    if (assemblys.AllKeys.Contains(modelName))
                        assemblyName = assemblys.Get(modelName);
                    if (!string.IsNullOrEmpty(assemblyName))
                    {
                        result = Assembly.Load(assemblyName).CreateInstance(string.Format("{0}.{1}", assemblyName, modelName));
                        if (result != null)
                            _assemblyDict[dictKey] = result;
                    }
                    else
                        throw new Exception("读取配置文件出错！");

                }
                return (T)result;
            }
            catch (Exception ex)
            {
                throw new Exception("加载程序集[" + modelName + "]出错！" + ex);
            }
        }
    }
}
