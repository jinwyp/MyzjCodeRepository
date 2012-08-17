using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class MDataTypeUtiity
    {
        /// <summary>
        /// 复制对象
        /// </summary>
        /// <typeparam name="T1"> </typeparam>
        /// <typeparam name="T2"> </typeparam>
        /// <param name="from">源实例</param>
        /// <param name="to">目标实例</param>
        private static void CopyObject<T1, T2>(T1 from, T2 to)
        {

            var fieldsFrom = from.GetType().GetProperties();
            var fieldsTo = to.GetType().GetProperties();

            foreach (var fieldFrom in fieldsFrom)
            {
                var obj = fieldFrom.GetValue(from, null);
                foreach (var fieldTo in fieldsTo)
                {
                    if (fieldTo.Name == fieldFrom.Name)
                    {
                        fieldTo.SetValue(to, obj, null);
                        break;
                    }
                }
            }

        }
    }
}
