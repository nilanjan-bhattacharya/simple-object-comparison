using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectComparer
{
    public static class Comparer
    {
        public static bool AreSimilar<T>(T first, T second)
        {
            return IsEqual(first, second);
        }

        private static bool IsEqual(object first, object second)
        {
            bool flag = true;

            var propertyType1 = GetDataTypes(first);

            if (propertyType1 == DataTypes.Primitive)
            {
                flag &= first.ToString() == second.ToString();
            }
            else if (propertyType1 == DataTypes.Array)
            {
                flag &= IsArrayOrListSimmilar(first, second);
            }
            else if (propertyType1 == DataTypes.IEnumerable)
            {
                flag &= IsArrayOrListSimmilar(first, second);
            }
            else if (propertyType1 == DataTypes.Dictionary)
            {
                flag &= IsDictionarySimilar(first, second);
            }
            else
            {
                var properties = first.GetType().GetProperties();

                foreach (var prop in properties)
                {

                    var f = first.GetType().GetProperty(prop.Name).GetValue(first, null);
                    var s = second.GetType().GetProperty(prop.Name).GetValue(second, null);

                    flag &= IsEqual(f, s);

                }

            }

            return flag;

        }

        private static bool IsDictionarySimilar(object first, object second)
        {
            var fd = first as IDictionary;
            var sd = second as IDictionary;

            foreach(var i in fd.Keys)
            {
                if(sd[i] != fd[i])
                {
                    return false;
                }

            }
            return true;
        }

        private static bool IsArrayOrListSimmilar(object f, object s)
        {
            var obj1 = (f as IEnumerable) ?? (f as Array);
            var obj2 = (s as IEnumerable) ?? (s as Array);


            List<string> firstToList = new List<string>();

            foreach (var i in obj1)
            {
                firstToList.Add(i.ToString());
            }

            foreach (var i in obj2)
            {
                if (!firstToList.Contains(i.ToString()))
                {
                    return false;
                }
            }

            return true;
        }

        private static DataTypes GetDataTypes(object o)
        {

            if (o is int || o is float || o is decimal || o is double || o is string)
            {
                return DataTypes.Primitive;
            }
            else if (o is Array)
            {
                return DataTypes.Array;
            }
            else if (o as IDictionary != null)
            {
                return DataTypes.Dictionary;
            }
            else if (o as IEnumerable != null)
            {
                return DataTypes.IEnumerable;
            }
            else
            {
                return DataTypes.SimpleClass;
            }
        }
        private enum DataTypes
        {
            Primitive,
            Array,
            IEnumerable,
            Dictionary,
            SimpleClass
        }


    }
}
