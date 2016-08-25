using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Celeste
{
    /// <summary>
    /// A suite of extra extension functions which will come in handy
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Rather than doing a reference check for generic objects this will check values.
        /// For primitives, it will cast the objects to each primitive type in an attempt to find a match.
        /// It will also attempt to cast the objects to lists and then call ValueEquals on their contents (requires them to be in the same order).
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="otherObj"></param>
        /// <returns></returns>
        public static bool ValueEquals(this object obj, object otherObj)
        {
            if (obj is string && otherObj is string)
            {
                return (string)obj == (string)otherObj;
            }

            if (obj is char && otherObj is char)
            {
                return (char)obj == (char)otherObj;
            }

            string s_result1 = obj.ToString(), s_result2 = otherObj.ToString();

            float f_result1, f_result2;
            if (float.TryParse(s_result1, out f_result1) && float.TryParse(s_result2, out f_result2))
            {
                return f_result1 == f_result2;
            }

            bool b_result1, b_result2;
            if (bool.TryParse(s_result1, out b_result1) && bool.TryParse(s_result2, out b_result2))
            {
                return b_result1 == b_result2;
            }

            List<object> l_result1, l_result2;
            if (obj.IsList() && otherObj.IsList())
            {
                l_result1 = obj as List<object>;
                l_result2 = otherObj as List<object>;

                if (l_result1.Count != l_result2.Count)
                {
                    return false;
                }

                for (int i = 0; i < l_result1.Count; i++)
                {
                    if (!l_result1[i].ValueEquals(l_result2[i]))
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        public static bool IsNumber(this object obj)
        {
            float result;
            return float.TryParse(obj.ToString(), out result);
        }

        public static bool IsString(this object obj)
        {
            return obj is string;
        }

        public static bool IsBool(this object obj)
        {
            bool result;
            return bool.TryParse(obj.ToString(), out result);
        }

        public static bool IsList(this object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Type type = obj.GetType();

            if (type.IsPrimitive)
            {
                return false;
            }

            return obj is IList &&
                   type.IsGenericType &&
                   type.GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
        }

        public static object GetValue(this Dictionary<object, object> dictionary, object key)
        {
            object value = null;
            if (dictionary.TryGetValue(key, out value))
            {
                return value;
            }
            
            foreach (object dictKey in dictionary.Keys)
            {
                if (dictKey.ValueEquals(key))
                {
                    return dictionary[dictKey];
                }
            }

            return null;
        }
    }
}