using System.Collections.Generic;
using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// The keyword for creating a global variable in the global scope - useful for namespacing.
    /// Globals survive over scripts
    /// </summary>
    internal class GlobalKeyword : Keyword
    {
        internal static string scriptToken = "global";

        #region Virtual Functions

        public override void Compile(CompiledStatement parent, string token, LinkedList<string> tokens, LinkedList<string> lines)
        {
            base.Compile(parent, token, tokens, lines);

            // Now check that there is another element after our keyword that we can use as the variable name
            Debug.Assert(tokens.Count > 0, "No value found for the right hand side of keyword: " + token);

            // Get the next token that appears on the right hand side of this operator - this will be our variable name
            string rhsOfKeyword = CelesteCompiler.PopToken();

            if (rhsOfKeyword == FunctionKeyword.scriptToken)
            {
                Debug.Assert(tokens.Count > 0, "Function name must exist");
                string functionName = tokens.First.Value;

                int parameterStartDelimiterIndex = functionName.IndexOf(FunctionKeyword.parameterStartDelimiter);
                Debug.Assert(parameterStartDelimiterIndex >= 0, "Incorrect function formatting.");
                functionName = functionName.Substring(0, parameterStartDelimiterIndex);

                if (CelesteCompiler.CompileToken(rhsOfKeyword, parent))
                {
                    // If we have compiled our global function, we need to remove it from the current scope and move it to the global scope
                    Debug.Assert(CelesteStack.CurrentScope.VariableExists(functionName));
                    Variable function = CelesteStack.CurrentScope.RemoveLocalVariable(functionName);
                    CelesteStack.GlobalScope.AddLocalVariable(function);
                }
                else
                {
                    Debug.Fail("Error parsing global function");
                }

                return;
            }

            Debug.Assert(!CelesteStack.GlobalScope.VariableExists(rhsOfKeyword), "Variable with the same name already exists in this scope");

            // Creates a new variable, but does not call Compile - Compile for variable assigns a reference from the stored variable in CelesteStack
            Variable variable = CelesteStack.GlobalScope.AddLocalVariable<Variable>(rhsOfKeyword);

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
