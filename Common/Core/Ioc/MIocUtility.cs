using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Context.Support;
using Spring.Context;
using Spring.Objects.Factory;

namespace Core.Ioc
{
    public static class MIocUtility
    {
        public static IApplicationContext SpringContext;

        /// <summary>
        /// 构造函数
        /// </summary>
        static MIocUtility()
        {
            if (SpringContext == null)
            {
                SpringContext = ContextRegistry.GetContext();
            }
        }

        /// <summary>
        /// 获取注入对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T GetObject<T>(string name)
        {
            return SpringContext.GetObject<T>(name);
        }
    }
}
