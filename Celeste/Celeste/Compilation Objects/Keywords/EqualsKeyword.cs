using System.Collections.Generic;
using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// A keyword for the logical equality operator '=='
    /// </summary>
    internal class EqualsKeyword : Keyword
    {
        internal static string scriptToken = "equals";

        #region Virtual Functions

        /// <summary>
        /// Adds an equality binary operator with the previous compiled statement and next token's compiled object to the parent
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="token"></param>
        /// <param name="tokens"></param>
        /// <param name="lines"></param>
        public override void Compile(CompiledStatement parent, string token, LinkedList<string> tokens, LinkedList<string> lines)
        {
            base.Compile(parent, token, tokens, lines);

            Debug.Assert(parent.ChildCount > 0, "Must have an object on the left side of the 'equals' keyword");

            // Now check that there is another element after our keyword that we can use as the variable name
            Debug.Assert(tokens.Count > 0, "No value found for the right hand side of the 'equals' keyword");

            // Create an equality operator and compile - this will do all the work of moving statements around in the parent
            // It will also add itself to the parent tree
            EqualityOperator equalityOperator = new EqualityOperator();
            equalityOperator.Compile(parent, EqualityOperator.scriptToken, tokens, lines);
        }

        #endregion
    }
}
