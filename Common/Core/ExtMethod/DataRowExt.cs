using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace Core.ExtMethod
{
    public static class DataRowExt
    {
        /// <summary>
        /// DataRow 转 实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static T DataRowToEntity<T>(this DataRow dr) where T : new()
        {
            T entity = new T();
            //以下方法也可以
            //T entity = System.Activator.CreateInstance<T>();
            Type t = typeof(T);
            PropertyInfo[] pinfos = t.GetProperties();
            foreach (var pinfo in pinfos)
            {
                Type vt = pinfo.PropertyType;
                if (dr.Table.Columns.Contains(pinfo.Name))
                {
                    var _val = Convert.ChangeType(dr[pinfo.Name], vt);
                    pinfo.SetValue(entity, _val, null);
                }
            }
            return entity;
        }
    }
}
