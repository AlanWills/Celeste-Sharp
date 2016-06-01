using System.Collections.Generic;
using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// The keyword for creating a local variable in the current relevant scope
    /// </summary>
    internal class ScopedKeyword : Keyword
    {
        internal static string scriptToken = "scoped";

        #region Virtual Functions

        public override void Compile(CompiledStatement parent, string token, LinkedList<string> tokens, LinkedList<string> lines)
        {
            base.Compile(parent, token, tokens, lines);

            // Now check that there is another element after our keyword that we can use as the variable name
            Debug.Assert(tokens.Count > 0, "No value found for the right hand side of keyword: " + token);

            // Get the next token that appears on the right hand side of this operator - this will be our variable name
            string rhsOfKeyword = CelesteCompiler.PopToken();
            
            Debug.Assert(!CelesteStack.CurrentScope.VariableExists(rhsOfKeyword), "Variable with the same name already exists in this scope");

            // Creates a new variable, but does not call Compile - Compile for variable assigns a reference from the stored variable in CelesteStack
            Variable variable = CelesteStack.CurrentScope.AddLocalVariable<Variable>(rhsOfKeyword);

            // If we still have unparsed tokens, then add this variable to the tree
            // Otherwise, do not - we do not need to push an object onto the stack for no reason
            if (tokens.Count > 0)
            {
                // Add our variable underneath our parent - this keyword will do nothing at run time so do not add it to the tree
                parent.Add(variable);
            }
        }

        #endregion
    }
}