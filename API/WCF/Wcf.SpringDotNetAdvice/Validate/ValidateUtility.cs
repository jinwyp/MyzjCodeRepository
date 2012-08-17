using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DataTypeUtility;

namespace Wcf.SpringDotNetAdvice.Validate
{
    public class ValidateUtility
    {
        /// <summary>
        /// 检查null 值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object CheckNull(object obj)
        {
            object result = obj;
            if (obj != null && obj is String)
            {
                try
                {
                    var val = obj.ToString();
                    var iVal = MCvHelper.To<int>(val, -1);
                    if (iVal == -1)
                    {
                        var verifyStrs = new string[] { "NULL", "_" };
                        if (!verifyStrs.Contains(val.ToUpper()))
                        {
                            result = val;
                        }
                        else
                            result = string.Empty;
                    }
                }
                catch
                {
                }
            }
            return result;
        }
    }
}
