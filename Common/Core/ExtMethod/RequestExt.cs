using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Core.ExtMethod
{
    public static class RequestExt
    {
        /// <summary>
        /// 获取参数值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <param name="paramName"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static T mGetParam<T>(this HttpRequest request, string paramName, params T[] def)
        {
            string _val = request[paramName];
            return _val.MConvertTo<T>(def);
        }

        /// <summary>
        /// 获取数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static T[] mGetParams<T>(this HttpRequest request, string paramName)
        {
            string _val = request[paramName];
            return _val.MConvertToArray<T>();
        }

    }
}
