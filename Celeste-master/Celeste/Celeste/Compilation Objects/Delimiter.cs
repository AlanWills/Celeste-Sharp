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
        internal static char scriptTokenChar = ',';
        internal static string InlineToken = "";

        public static bool HasDelimiter(string token)
        {
            return token.Contains(scriptToken);
        }

        #region Virtual Functions

        public override void Compile(CompiledStatement parent, string token, LinkedList<string> tokens, LinkedList<string> lines)
        {
            base.Compile(parent, token, tokens, lines);

            // Find the first occurrence of the delimiter and split the string at that index
            int delimiterIndex = token.IndexOf(scriptTokenChar);
            Debug.Assert(delimiterIndex > 0);   // Must exist and cannot be the first element in a string

            // Split the token into two strings - the lhs of the first delimiter and the remaining rhs after the delimiter
            string lhsOfDelimiter = token.Substring(0, delimiterIndex);
            string rhsOfDelimiter = token.Substring(delimiterIndex + 1);

            // We add these in the reverse order so that they appear as LHS, INLINE, RHS (the tokens list is basically a stack)

            // Add anything leftover on the rhs
            if (!string.IsNullOrEmpty(rhsOfDelimiter))
            {
                tokens.AddFirst(rhsOfDelimiter);
            }

            // Add our inlined token
            tokens.AddFirst(InlineToken);

            // Add the lhs of delimiter - this cannot be empty
            Debug.Assert(!string.IsNullOrEmpty(lhsOfDelimiter));
            tokens.AddFirst(lhsOfDelimiter);
        }

        public override void PerformOperation()
        {
            Debug.Fail("Delimiter is a compile time only object");
        }

        #endregion
    }
}
