using System.Collections.Generic;
using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// An operator which acts on the next token in our script
    /// </summary>
    internal class UnaryOperator : CompiledStatement
    {
        #region Virtual Functions

        public override void Compile(CompiledStatement parent, string token, LinkedList<string> tokens, LinkedList<string> lines)
        {
            base.Compile(parent, token, tokens, lines);

            // Add this operator to the tree
            parent.Add(this);

            // Now check that there is another element after our operator that we can act on
            Debug.Assert(tokens.Count > 0, "No value found for the right hand side of operator: " + token);

            // Get the next token that appears on the right hand side of this operator
            string rhsOfOperatorToken = CelesteCompiler.PopToken();

            // Parse the next token which we will act on
            if (CelesteCompiler.CompileToken(rhsOfOperatorToken))
            {
                // Take the value that has been added to the root and add it under this operator instead
                parent.MoveChildAtIndex(parent.ChildCount - 1, this);
            }
            else
            {
                // Error message if we cannot parse the next token
                Debug.Fail("Could not compile token: " + token + " in operator " + token);
            }
        }

        #endregion
    }
}
