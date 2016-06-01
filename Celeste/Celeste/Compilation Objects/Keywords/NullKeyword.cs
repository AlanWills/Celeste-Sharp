using System.Collections.Generic;
using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// The keyword that represents the null value within our language
    /// </summary>
    internal class NullKeyword : Keyword
    {
        internal static string scriptToken = "null";

        #region Virtual Functions

        public override void Compile(CompiledStatement parent, string token, LinkedList<string> tokens, LinkedList<string> lines)
        {
            base.Compile(parent, token, tokens, lines);

            Debug.Assert(parent.ChildCount > 0, "No object on the left hand side of the 'null' keyword");

            // Create a value object with null as the stored value - it will be moved under the equality operator when we call nullValue.Compile
            // This will push null onto the stack when run
            Value nullValue = new Value(null);
            nullValue.Compile(parent, token, tokens, lines);
        }

        #endregion
    }
}