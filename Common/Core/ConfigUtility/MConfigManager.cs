using System;
using System.Linq;
using System.Configuration;
using Core.DataTypeUtility;
using Core.Enums;
using Core.LogUtility;
using System.Web;
using System.Web.Configuration;

namespace Core.ConfigUtility
{
    /// <summary>
    /// 配置文件
    /// </summary>
    public static class MConfigManager
    {
        private static readonly Configuration Config;

        /// <summary>
        /// 构造函数
        /// </summary>
        static MConfigManager()
        {
            try
            {
                var httpContext = HttpContext.Current;
                if (httpContext != null)
                    Config = WebConfigurationManager.OpenWebConfiguration("~");
                else
                    Config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Other.配置文件操作, null, null, "初始化配置文件错误", ex);
            }
        }

        /// <summary>
        /// 解析 Config Key
        /// </summary>
        /// <param name="configsCategory"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string FormatKey(string key, MConfigs.ConfigsCategory configsCategory)
        {
            return string.Format("{0}_{1}", arg0: configsCategory, arg1: key);
        }

        /// <summary>
        /// 获取 配置文件 AppSettings 节点的值
        /// </summary>
        /// <param name="key">配置文件的Key</param>
        /// <param name="def">如果不存在</param>
        /// <returns></returns>
        public static T GetAppSettingsValue<T>(string key, params T[] def)
        {
            object val = null;
            try
            {
                if (Config.AppSettings.Settings.AllKeys.Contains(key))
                {
                    val = Config.AppSettings.Settings[key].Value;
                }
                else if (def.Length > 0)
                {
                    if (AddAppSettings(key, def[0].ToString()))
                        val = def[0];
                }
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Other.配置文件操作, null, null, string.Format("获取 配置文件 AppSettings 节点 {0}={1}", key, def[0]), ex);
            }
            return MCvHelper.To<T>(val);
        }

        /// <summary>
        /// 获取 配置文件 AppSettings 节点的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="configsCategory"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static T GetAppSettingsValue<T>(string key, MConfigs.ConfigsCategory configsCategory, params T[] def)
        {
            return GetAppSettingsValue<T>(FormatKey(key, configsCategory), def);
        }

        /// <summary>
        /// 添加 配置文件 AppSettings 节点
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool AddAppSettings(string key, string val)
        {
            bool result = false;
            try
            {
                Config.AppSettings.Settings.Add(key, val);
                Config.Save();
                result = true;
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Other.配置文件操作, null, null, string.Format("添加 配置文件 AppSettings 节点 {0}={1}", key, val), ex);
            }
            return result;
        }

        /// <summary>
        ///  添加 配置文件 AppSettings 节点
        /// </summary>
        /// <param name="key"></param>
        /// <param name="configsCategory"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool AddAppSettings(string key, MConfigs.ConfigsCategory configsCategory, string val)
        {
            return AddAppSettings(FormatKey(key, configsCategory), val);
        }

        /// <summary>
        /// 设置 配置文件 AppSettings 节点的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool SetAppSettingsValue(string key, string val)
        {
            var result = false;
            try
            {
                RemoveAppSettings(key);
                Config.AppSettings.Settings.Add(key, val);
                Config.Save();
                result = true;
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Other.配置文件操作, null, null, string.Format("设置 配置文件 AppSettings 节点 {0}={1}", key, val), ex);
            }
            return result;
        }

        /// <summary>
        /// 设置 配置文件 AppSettings 节点的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="configsCategory"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool SetAppSettingsValue(string key, MConfigs.ConfigsCategory configsCategory, string val)
        {
            return SetAppSettingsValue(FormatKey(key, configsCategory), val);
        }

        /// <summary>
        /// 移除 配置文件 AppSettings 节点
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool RemoveAppSettings(string key)
        {
            var result = false;
            try
            {
                if (Config.AppSettings.Settings.AllKeys.Contains(key))
                {
                    Config.AppSettings.Settings.Remove(key);
                    Config.Save();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Other.配置文件操作, null, null, string.Format("移除 配置文件 AppSettings 节点 {0}", key), ex);
            }
            return result;
        }

        /// <summary>
        /// 移除 配置文件 AppSettings 节点
        /// </summary>
        /// <param name="key"></param>
        /// <param name="configsCategory"></param>
        /// <returns></returns>
        public static bool RemoveAppSettings(string key, MConfigs.ConfigsCategory configsCategory)
        {
            return RemoveAppSettings(FormatKey(key, configsCategory));
        }

    }
}
