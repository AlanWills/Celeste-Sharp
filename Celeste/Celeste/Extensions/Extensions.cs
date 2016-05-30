﻿using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// A suite of extra extension functions which will come in handy
    /// </summary>
    public static class Extensions
    {
        public static bool ValueEquals(this object obj, object otherObj)
        {
            if (!obj.GetType().IsPrimitive || !otherObj.GetType().IsPrimitive)
            {
                return false;
            }

            if (obj is string && otherObj is string)
            {
                return (string)obj == (string)otherObj;
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

            return false;
        }

        public static bool IsNumber(this object obj)
        {
            float result;
            return float.TryParse(obj.ToString(), out result);
        }

        public static bool IsString(this object obj)
        {
            return obj is string || obj is char;
        }

        public static bool IsBool(this object obj)
        {
            bool result;
            return bool.TryParse(obj.ToString(), out result);
        }
    }
}