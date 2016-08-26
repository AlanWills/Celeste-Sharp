using System.Collections.Generic;

namespace Celeste
{
    /// <summary>
    /// This is used to edit our compile tree at compile time to affect the order of execution of various operators.
    /// Also used in function arguments and invocations to group parameters.
    /// </summary>
    internal class CloseParenthesis : CompiledStatement
    {
        internal static string scriptToken = ")";
        internal static char scriptTokenChar = ')';

        public static bool HasDelimiter(string token)
        {
            // If it's just the delimiter we don't want to call compile on it as the token - we are going to use it as a marker for other things to pick up in our tokens list
            return token.Contains(scriptToken) && token.Length > 1;
        }

        #region Virtual Functions

        public override void Compile(CompiledStatement parent, string token, LinkedList<string> tokens, LinkedList<string> lines)
        {
            base.Compile(parent, token, tokens, lines);

            // For now we will just take the parenthesis and add it as a separate token along with the rest of the token
            // Need to do actual implementation stuff though
            int index = token.IndexOf(scriptToken);
            string lhsString = token.Substring(0, index);
            string rhsString = token.Substring(index + 1);

            // Split the string with the parenthesis but maintain the order of the tokens in the list from left to right, first to last

            if (!string.IsNullOrEmpty(rhsString))
            {
                tokens.AddFirst(rhsString);
            }

            tokens.AddFirst(scriptToken);

            if (!string.IsNullOrEmpty(lhsString))
            {
                tokens.AddFirst(lhsString);
            }
        }

        #endregion
    }
}
