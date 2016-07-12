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

            // Unary operators act on other variables/values and so they must be included in the same token as another token (e.g. !var for example)
            // We remove the script token for the operator and split the other token out
            Debug.Assert(token.Length > 1);
            string rest = token.Remove(0, 1);

            // Add this operator to the tree
            parent.Add(this);

            // Parse the rest of the token which we will act on
            if (CelesteCompiler.CompileToken(rest, parent))
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
