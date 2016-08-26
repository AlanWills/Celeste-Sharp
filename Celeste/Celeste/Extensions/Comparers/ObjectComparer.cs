using System.Collections.Generic;

namespace Celeste
{
    internal class ObjectComparer : IEqualityComparer<object>
    {
        public new bool Equals(object x, object y)
        {
            return x.Equals(y) || x.ValueEquals(y);
        }

        public int GetHashCode(object obj)
        {
            return obj.GetHashCode();
        }
    }
}