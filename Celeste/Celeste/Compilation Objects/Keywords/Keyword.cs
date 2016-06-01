using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// A class to represent a generic keyword 
    /// </summary>
    internal class Keyword : CompiledStatement
    {
        #region Virtual Functions

        /// <summary>
        /// Keywords are compile time objects only - they are resolved into an expression which is then added to our compile tree
        /// Therefore their perform operation should never be called and not be overridable
        /// </summary>
        public sealed override void PerformOperation()
        {
            Debug.Fail("Keywords should not exist at runtime - they are compile time resolved into an expression");
        }

        #endregion
    }
}
