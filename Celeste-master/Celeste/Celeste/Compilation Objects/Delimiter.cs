using System.Collections.Generic;
using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// This inlines itself at compile time as the most recent keyword which supports
    /// same-line delimiter declarations (e.g. variable declaration, function parameters etc.)
    /// </summary>
    internal class Delimiter : CompiledStatement
    {
        internal static string scriptToken = ",";
        internal static string inlineToken = "";

        public static bool IsDelimiter(string token)
        {
            return token.EndsWith(scriptToken);
        }

        #region Virtual Functions

        public override void Compile(CompiledStatement parent, string token, LinkedList<string> tokens, LinkedList<string> lines)
        {
            base.Compile(parent, token, tokens, lines);

            // Add our inlined token
            tokens.AddFirst(inlineToken);

            // Add anything leftover - e.g. scoped var = 5, var2 = 10
            // we will want to change to scoped var = 5 scoped var2 = 10
            string leftoverToken = token.Remove(token.Length - 1);
            if (!string.IsNullOrEmpty(leftoverToken))
            {
                tokens.AddFirst(leftoverToken);
            }
        }

        public override void PerformOperation()
        {
            Debug.Fail("Delimiter is a compile time only object");
        }

        #endregion
    }
}
