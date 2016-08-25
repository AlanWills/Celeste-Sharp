using System.Collections.Generic;
using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// The keyword used to indicate that we are exiting a function and optionally returning arguments from a function
    /// </summary>
    internal class ReturnKeyword : Keyword
    {
        internal static string scriptToken = "return";
        private static string returnParameterDelimiter = ",";

        #region Virtual Functions

        public override void Compile(CompiledStatement parent, string token, LinkedList<string> tokens, LinkedList<string> lines)
        {
            base.Compile(parent, token, tokens, lines);

            parent.Add(this);

            while (tokens.Count > 0)
            {
                // Get the next token that appears on the right hand side of this operator
                // This will be the name of the local variables we wish to return
                string rhsOfKeyword = CelesteCompiler.PopToken();
                if (rhsOfKeyword.EndsWith(returnParameterDelimiter))
                {
                    // Return the parameter delimiter after each return parameter if it exists
                    rhsOfKeyword = rhsOfKeyword.Remove(rhsOfKeyword.Length - 1);
                }
                Debug.Assert(CelesteCompiler.CompileToken(rhsOfKeyword, this), "Error compiling return parameter");
            }
        }

        #endregion
    }
}
