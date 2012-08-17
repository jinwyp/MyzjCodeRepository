using System;

namespace Core.ExtMethod
{
    public static class MainExt
    {
        public static ExtBase MyExt(object obj)
        {
            Type _type = obj.GetType();
            ExtBase _extobj = null;

            if (_type.Equals(typeof(string)))
            { }
            else if (_type.Equals(typeof(string)))
            { }
            else if (_type.Equals(typeof(short)))
            { }
            else if (_type.Equals(typeof(int)))
            { }
            else if (_type.Equals(typeof(long)))
            { }
            else if (_type.Equals(typeof(decimal)))
            { }
            else if (_type.Equals(typeof(double)))
            { }
            else if (_type.Equals(typeof(char)))
            { }
            else if (_type.IsClass)
            { }
            else if (_type.IsEnum)
            { }
            else
            { }

            return _extobj;
        }
    }

}
