using System.Collections.Generic;
using System.Diagnostics;

namespace Celeste
{
    internal class WhileFlowControl : FlowControl
    {
        private static string scriptToken = "while";
        private static string endDelimiter = "end";

        public static bool IsWhileFlowControl(string token)
        {
            return token == scriptToken;
        }

        #region Virtual Functions

        /// <summary>
        /// Reads in the condition for the while loop and then the body to perform while the condition is true.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="token"></param>
        /// <param name="tokens"></param>
        /// <param name="lines"></param>
        public override void Compile(CompiledStatement parent, string token, LinkedList<string> tokens, LinkedList<string> lines)
        {
            base.Compile(parent, token, tokens, lines);

            KeyValuePair<CompiledStatement, CompiledStatement> whileCondBody = new KeyValuePair<CompiledStatement, CompiledStatement>(new CompiledStatement(), new CompiledStatement());
            ConditionsAndBodies.Add(whileCondBody);

            Debug.Assert(tokens.Count > 0, "Tokens required for while condition");
            while (tokens.Count > 0)
            {
                CelesteCompiler.CompileToken(CelesteCompiler.PopToken(), whileCondBody.Key);
            }

            // We keep parsing until we find the closing keyword for our while control
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
                else if (CelesteCompiler.CompileToken(nextToken, whileCondBody.Value))
                {

                }
                else
                {
                    // Error message if we cannot parse the next token
                    Debug.Fail("Operator invalid on token: " + token);
                }
            }

            // Close the scope that is automatically opened in our constructor
            CelesteStack.CurrentScope = FlowScope.ParentScope;
        }

        #endregion
    }
}