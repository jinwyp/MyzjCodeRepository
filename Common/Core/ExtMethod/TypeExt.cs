using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.ExtMethod
{
    public static class TypeExt
    {

        /*
            [TestMethod()]
     public void IsNullableTypeTest()
     {
         Assert.AreEqual(true, TypeExtension.IsNullableType(typeof(int?)));
         Assert.AreEqual(false, TypeExtension.IsNullableType(typeof(int)));
         Assert.AreEqual(true, TypeExtension.IsNullableType(typeof(Nullable<DateTime>)));
         Assert.AreEqual(false, TypeExtension.IsNullableType(typeof(DateTime)));
     }
     [TestMethod()]
     public void GetNonNullableTypeTest()
     {
         Assert.AreEqual(typeof(int), TypeExtension.GetNonNullableType(typeof(int?)));
         Assert.AreEqual(typeof(DateTime), TypeExtension.GetNonNullableType(typeof(Nullable<DateTime>)));
     }
     [TestMethod()]
     public void IsEnumerableTypeTest()
     {
         Assert.AreEqual(true, TypeExtension.IsEnumerableType(typeof(IEnumerable<string>)));
         Assert.AreEqual(true, TypeExtension.IsEnumerableType(typeof(Collection<int>)));
     }
     [TestMethod()]
     public void GetElementTypeTest()
     {
         Assert.AreEqual(typeof(int), TypeExtension.GetElementType(typeof(IEnumerable<int>)));
         Assert.AreEqual(typeof(DateTime), TypeExtension.GetElementType(typeof(Collection<DateTime>)));
     }
     [TestMethod()]
     public void IsKindOfGenericTest()
     {
         Assert.AreEqual(true, TypeExtension.IsKindOfGeneric(typeof(List<string>), typeof(IEnumerable<>)));
         Assert.AreEqual(true, TypeExtension.IsKindOfGeneric(typeof(string), typeof(IComparable<>)));
     }
     [TestMethod()]
     public void FindGenericTypeTest()
     {
         Assert.AreEqual(typeof(IEnumerable<string>),
         TypeExtension.FindGenericType(typeof(IEnumerable<>), typeof(List<string>)));
     }
         */

        public static bool IsNullableType(this Type type)
        {
            return (((type != null) && type.IsGenericType) &&
                (type.GetGenericTypeDefinition() == typeof(Nullable<>)));
        }

        public static Type GetNonNullableType(this Type type)
        {
            if (IsNullableType(type))
            {
                return type.GetGenericArguments()[0];
            }
            return type;
        }

        public static bool IsEnumerableType(this Type enumerableType)
        {
            return (FindGenericType(typeof(IEnumerable<>), enumerableType) != null);
        }

        public static Type GetElementType(this Type enumerableType)
        {
            Type type = FindGenericType(typeof(IEnumerable<>), enumerableType);
            if (type != null)
            {
                return type.GetGenericArguments()[0];
            }
            return enumerableType;
        }

        public static bool IsKindOfGeneric(this Type type, Type definition)
        {
            return (FindGenericType(definition, type) != null);
        }

        public static Type FindGenericType(this Type definition, Type type)
        {
            while ((type != null) && (type != typeof(object)))
            {
                if (type.IsGenericType && (type.GetGenericTypeDefinition() == definition))
                {
                    return type;
                }
                if (definition.IsInterface)
                {
                    foreach (Type type2 in type.GetInterfaces())
                    {
                        Type type3 = FindGenericType(definition, type2);
                        if (type3 != null)
                        {
                            return type3;
                        }
                    }
                }
                type = type.BaseType;
            }
            return null;
        }
    }
}
