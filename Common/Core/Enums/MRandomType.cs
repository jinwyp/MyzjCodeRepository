using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Enums
{
    [Flags]
    public enum MRandomType
    {
        All = UpperChar | LowerCarh | Num | Other,
        UpperChar = 1,
        LowerCarh = 2,
        Num = 4,
        Other = 8
    }
}
