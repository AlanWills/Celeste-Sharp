﻿using System.Collections.Generic;
using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// The keyword for declaring a function
    /// </summary>
    internal class FunctionKeyword : Keyword
    {
        #region Properties and Fields

        internal static string scriptToken = "function";
        private static string endDelimiter = "end";
        internal static char parameterStartDelimiter = '(';
        private static char parameterEndDelimiterChar = ')';
        private static string parameterEndDelimiter = ")";

        #endregion

        public override void Compile(CompiledStatement parent, string token, LinkedList<string> tokens, LinkedList<string> lines)
        {
            base.Compile(parent, token, tokens, lines);

            // Now check that there is another element after our keyword that we can use as the function name
            Debug.Assert(tokens.Count > 0 || lines.Count > 0, "No parameters or body found for the keyword: " + token);

            // Get the next token that appears on the right hand side of this operator - this will be our function name
            string rhsOfKeyword = CelesteCompiler.PopToken();

            int bracketOpening = rhsOfKeyword.IndexOf(parameterStartDelimiter);
            Debug.Assert(bracketOpening >= 0, "No '(' token in function declaration");

            string functionName = rhsOfKeyword.Substring(0, bracketOpening);

            Debug.Assert(!CelesteStack.CurrentScope.VariableExists(functionName), "Variable with the same name already exists in this scope");
            int currentIndex = parent.ChildCount;

            // Creates a new function, but does not call Compile - Compile for function assigns a reference from the stored function in CelesteStack
            Function function = CelesteStack.CurrentScope.CreateLocalVariable<Function>(functionName);

            // Obtain the parameter names from the strings/tokens between the brackets
            string parameters = rhsOfKeyword.Substring(bracketOpening + 1);
            tokens.AddFirst(parameters);
            List<string> paramNames = new List<string>();
            do
            {
                // We do this if our function arguments are separated by spaces
                parameters = CelesteCompiler.PopToken();
                if (parameters.EndsWith(parameterEndDelimiter))
                {
                    // Remove the end delimiter if needs be
                    parameters.Substring(0, parameters.Length - 1);
                }

                // Add any parameters which are of this form 'param1,param2'
                // TODO Need to remove delimiters and empty spaces
                paramNames.AddRange(parameters.Split(Delimiter.scriptTokenChar));

                // This algorithm should completely take care of any mix of parameters separated with a space or not
            }
            while (!parameters.EndsWith(parameterEndDelimiter));

            function.SetParameters(paramNames.ToArray());

            // We keep parsing until we find the closing keyword for our function
            bool foundClosing = false;
            while (!foundClosing)
            {
                Debug.Assert(tokens.Count > 0 || lines.Count > 0, "Function requires a closing " + endDelimiter);

                // Create a new set of tokens if we have run out for this line
                if (tokens.Count == 0)
                {
                    CelesteCompiler.TokenizeNextLine();
                }

                string nextToken = CelesteCompiler.PopToken();
                if (nextToken == endDelimiter)
                {
                    foundClosing = true;
                }
                else if (CelesteCompiler.CompileToken(nextToken, function.FuncImpl))
                {
                    
                }
                else
                {
                    // Error message if we cannot parse the next token
                    Debug.Fail("Operator invalid on token: " + token);
                }
            }
 
            // Do not add the function - it will be looked up to and called rather than pushed onto the stack (much like a variable)
            // Close the function's scope now - we have added all the appropriate variables to it
            CelesteStack.CurrentScope = function.FunctionScope.ParentScope;
        }
    }
}