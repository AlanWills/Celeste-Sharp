using System.Collections.Generic;

namespace Celeste
{
    /// <summary>
    /// Returns whether two elements from a table have the same key (either reference or value)
    /// </summary>
    internal class TableKeyComparer : IEqualityComparer<KeyValuePair<object, object>>
    {
        public bool Equals(KeyValuePair<object, object> x, KeyValuePair<object, object> y)
        {
            return x.Key.Equals(y.Key) || x.Key.ValueEquals(y.Key);
        }

        public int GetHashCode(KeyValuePair<object, object> obj)
        {
            return obj.Key.GetHashCode();
        }
    }
}
