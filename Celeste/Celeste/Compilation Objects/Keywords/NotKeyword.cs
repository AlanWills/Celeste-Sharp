using System.Collections.Generic;
using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// A keyword which will be compiled out to the not operator
    /// </summary>
    internal class NotKeyword : Keyword
    {
        internal static string scriptToken = "not";

        #region Virtual Functions

        /// <summary>
        /// Adds a not unary operator with the next token's compiled object to the parent
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="token"></param>
        /// <param name="tokens"></param>
        /// <param name="lines"></param>
        public override void Compile(CompiledStatement parent, string token, LinkedList<string> tokens, LinkedList<string> lines)
        {
            base.Compile(parent, token, tokens, lines);

            // Now check that there is an element after our keyword that we can use as the variable name
            Debug.Assert(tokens.Count > 0, "No value found for the right hand side of the 'not' keyword");

            // Create a not operator and compile - this will do all the work of moving statements around in the parent
            // It will also add itself to the parent tree
            NotOperator notOperator = new NotOperator();
            notOperator.Compile(parent, NotOperator.scriptToken, tokens, lines);
        }

        #endregion
    }
}
