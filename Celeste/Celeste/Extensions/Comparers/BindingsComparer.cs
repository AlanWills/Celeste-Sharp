using System.Collections.Generic;
using BindingPair = System.Tuple<System.Type, System.Type>;

namespace Celeste
{
    /// <summary>
    /// Comparer for us to be able to find binding pairs that do not match on reference, but rather on the types they are holding.
    /// </summary>
    public class BindingsComparer : EqualityComparer<BindingPair>
    {
        public override bool Equals(BindingPair x, BindingPair y)
        {
            return x.Item1 == y.Item1 && x.Item2 == y.Item2;
        }

        public override int GetHashCode(BindingPair obj)
        {
            return obj.Item1.GetHashCode() + obj.Item2.GetHashCode();
        }
    }

}
