using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Management;
using System.Text;

namespace Core.DataTypeUtility
{
    public class MEfUtility
    {
        /// <summary>
        /// 获取 Entity FrameWork 执行的 sql 代码
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static string GetQuerySql<T>(IQueryable<T> query) where T : EntityObject
        {
            var parents = query as ObjectQuery<T>;
            if (parents != null)
                return parents.ToTraceString();
            else
                return "没有sql!";
        }
    }
}
