using System.Collections.Generic;
using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// The keyword that represents the null value within our language
    /// </summary>
    internal class NullKeyword : Keyword
    {
        public override void Compile(CompiledStatement parent, string token, List<string> tokens)
        {
            base.Compile(parent, token, tokens);

            // The previous statement that was compiled HAS to be the equality operator
            // The expression HAS to be of the form x = null
            // There can be no other valid syntax for this keyword
            CompiledStatement equalityOperator = parent.ChildCompiledStatements.FindLast(x => x.GetType() == typeof(EqualityOperator));
            Debug.Assert(equalityOperator != null);
            Debug.Assert(parent.ChildCompiledStatements.FindLastIndex(x => x.GetType() == typeof(EqualityOperator)) == parent.ChildCount - 1);

            // Check that there are no other elements after our keyword
            Debug.Assert(tokens.Count == 0, "No other values should exist after keyword: " + token);

            // Add a value object underneath the equality operator object who's value is null
            // This will push null onto the stack when run and the local variable on the lhs of the equality operator will be set to null
            Value nullValue = new Value(null);
            nullValue.Compile(parent, token, tokens);
        }
    }
}