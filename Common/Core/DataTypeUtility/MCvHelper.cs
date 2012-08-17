using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Core.DataTypeUtility
{
    /// <summary>
    /// 
    /// </summary>
    public static class MCvHelper
    {
        /// <summary>
        /// 数据类型转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static T To<T>(object obj, params T[] def)
        {
            Type _t = typeof(T);
            bool IsError = false;
            object _obj = null;
            try
            {
                if (_t.IsEnum)
                {
                    _obj = Enum.Parse(typeof(T), obj.ToString(), true);
                }
                else
                {
                    _obj = Convert.ChangeType(obj, _t);
                }
            }
            catch
            {
                IsError = true;
            }
            finally
            {
                if (IsError || _obj == null)
                {
                    if (def != null && def.Length > 0)
                        _obj = def[0];
                    else
                        _obj = default(T);
                }
            }
            return (T)_obj;
        }

        /// <summary>
        /// 转换为数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T[] ToArray<T>(object obj)
        {
            Type _t = typeof(T);
            bool IsError = false;
            object _obj = null;
            try
            {
                string[] arrays = Convert.ToString(obj).Split(new char[] { ',', '|', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (arrays.Length > 0)
                {
                    _obj = Array.ConvertAll<string, T>(arrays, s =>
                    {
                        return (T)Convert.ChangeType(s, _t);
                    });
                }
            }
            catch
            {
                IsError = true;
            }
            finally
            {
                if (IsError || _obj == null)
                {
                    _obj = default(T);
                }
            }
            return (T[])_obj;
        }

        /// <summary>
        /// 对象 数据 拷贝
        /// </summary>
        /// <param name="source">源对象</param>
        /// <param name="destination">目的对象</param>
        /// <returns>是否有异常</returns>
        public static bool ObjectCopyTo(object source, object destination)
        {
            bool rel = true;
            if (destination == null)
            {
                throw new Exception("目标对象未初始化！");
            }
            PropertyInfo[] _sourceProperties = source.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            PropertyInfo[] _destinationProperties = destination.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (PropertyInfo sourcePropertie in _sourceProperties)
            {
                foreach (PropertyInfo destinationProperty in _destinationProperties)
                {
                    if (sourcePropertie.Name.ToLower().Equals(destinationProperty.Name.ToLower()))
                    {
                        try
                        {
                            destinationProperty.SetValue(destination, sourcePropertie.GetValue(source, null), null);
                            break;
                        }
                        catch
                        {
                            rel = false;
                        }
                    }
                }
            }
            return rel;
        }

    }
}
