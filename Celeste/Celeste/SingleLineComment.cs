using System.Collections.Generic;

namespace Celeste
{
    /// <summary>
    /// The class that handles commented that only span a single line.
    /// </summary>
    internal class SingleLineComment : CompiledStatement
    {
        private static string scriptToken = "//";

        internal static bool IsCommented(string token)
        {
            return token.Contains(scriptToken);
        }

        #region Virtual Functions

        public override void Compile(CompiledStatement parent, string token, LinkedList<string> tokens, LinkedList<string> lines)
        {
            base.Compile(parent, token, tokens, lines);

            tokens.Clear();
        }

        #endregion
    }
}
