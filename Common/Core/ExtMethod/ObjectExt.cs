using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.ExtMethod
{
    /// <summary>
    /// 
    /// </summary>
    public static class ObjectExt
    {
        /// <summary>
        /// 数据类型转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static T MConvertTo<T>(this object obj, params T[] def)
        {
            Type t = typeof(T);
            bool isError = false;
            object _obj = null;
            try
            {
                if (t.IsEnum)
                {
                    _obj = Enum.Parse(typeof(T), obj.ToString(), true);
                }
                else
                {
                    _obj = Convert.ChangeType(obj, t);
                }
            }
            catch
            {
                isError = true;
            }
            finally
            {
                if (isError || _obj == null)
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
        public static T[] MConvertToArray<T>(this object obj)
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
    }
}
