using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.ExtMethod
{
    public static class IEnumerableExt
    {
        //int sum2 = nums.Aggregate((a, c, i) => a + i % 2 == 0 ? c : 0);//10 + 30 + 50
        //int[] array = new int[] { 1, 3, 2, 3, 4, 5 };
        //原极限算法
        //int repeatedNum1 = array.Select((i, j) => i - j).Sum();
        //最新极限算法
        //int repeatedNum2 = array.Aggregate((a, n, i) => a + n - i);
        public static TSource Aggregate<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, int, TSource> func)
        {
            int index = 0;
            using (IEnumerator<TSource> enumerator = source.GetEnumerator())
            {
                enumerator.MoveNext();
                index++;
                TSource current = enumerator.Current;
                while (enumerator.MoveNext())
                    current = func(current, enumerator.Current, index++);
                return current;
            }

        }
    }
}
