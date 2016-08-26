using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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

        #endregion

        public override void Compile(CompiledStatement parent, string token, LinkedList<string> tokens, LinkedList<string> lines)
        {
            base.Compile(parent, token, tokens, lines);

            // Now check that there is another element after our keyword that we can use as the function name
            Debug.Assert(tokens.Count > 0 || lines.Count > 0, "No parameters or body found for the keyword: " + token);

            // The first token should be the function name
            string functionName = CelesteCompiler.PopToken();
            Debug.Assert(!CelesteStack.CurrentScope.VariableExists(functionName), "Variable with the same name already exists in this scope");

            // Get the next token - this should be an opening parenthesis
            string openingParenthesis = CelesteCompiler.PopToken();
            Debug.Assert(openingParenthesis == OpenParenthesis.scriptToken, "No opening parenthesis found for function " + functionName);

            // Creates a new function, but does not call Compile - Compile for function assigns a reference from the stored function in CelesteStack
            Function function = CelesteStack.CurrentScope.CreateLocalVariable<Function>(functionName);

            // Obtain the parameter names from the strings/tokens between the brackets
            List<string> paramNames = new List<string>();
            string parameters = CelesteCompiler.PopToken();

            while (parameters != CloseParenthesis.scriptToken)
            {
                // Clean the parameter array of empty strings and remove any delimiter characters from parameter names
                List<string> parameterList = new List<string>(parameters.Split(Delimiter.scriptTokenChar));
                parameterList.RemoveAll(x => string.IsNullOrEmpty(x));
                parameterList = new List<string>(parameterList.Select(x => x = x.Replace(Delimiter.scriptToken, "")));

                // Add any parameters which are of this form 'param1,param2'
                paramNames.AddRange(parameterList);

                parameters = CelesteCompiler.PopToken();// This algorithm should completely take care of any mix of parameters separated with a space or not
            }

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
            // This scope is automatically opened in the Function constructor
            CelesteStack.CurrentScope = function.FunctionScope.ParentScope;
        }
    }
}
