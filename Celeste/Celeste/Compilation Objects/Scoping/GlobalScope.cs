namespace Celeste
{
    /// <summary>
    /// A special implementation of scope which constitutes the base scope for all other scopes
    /// </summary>
    internal class GlobalScope : Scope
    {
        internal GlobalScope() :
            base(null)
        {

        }
    }
}
