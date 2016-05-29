using System.Collections.Generic;
using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// An abstract class which represents an operator which acts on two objects and returns an object of the same type
    /// </summary>
    internal abstract class BinaryOperator : CompiledStatement
    {
        #region Virtual Functions

        public override void Compile(CompiledStatement parent, string token, List<string> tokens)
        {
            base.Compile(parent, token, tokens);

            Debug.Assert(parent.ChildCount > 0, "No value found for the left hand side of operator: " + token);

            // Take the previous statement and add it as a sub statement of the newly created operator - we will check for validity in the Compile function
            parent.MoveChildAtIndex(parent.ChildCount - 1, this);

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
                Debug.Fail("Operator invalid on token: " + token);
            }
        }

        #endregion
    }
}
