using System.Collections.Generic;
using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// The keyword that represents the null value within our language
    /// </summary>
    internal class NullKeyword : Keyword
    {
        public override void Compile(CompiledStatement parent, string token, LinkedList<string> tokens, LinkedList<string> lines)
        {
            base.Compile(parent, token, tokens, lines);

            // The previous statement that was compiled HAS to be the equality operator
            // The expression HAS to be of the form x = null
            // There can be no other valid syntax for this keyword
            CompiledStatement equalityOperator = parent.ChildCompiledStatements.FindLast(x => x.GetType() == typeof(AssignmentOperator));
            Debug.Assert(equalityOperator != null);
            Debug.Assert(parent.ChildCompiledStatements.FindLastIndex(x => x.GetType() == typeof(AssignmentOperator)) == parent.ChildCount - 1);

            // Check that there are no other elements after our keyword
            Debug.Assert(tokens.Count == 0, "No other values should exist after keyword: " + token);

            // Create a value object with null as the stored value - it will be moved under the equality operator when this function is called
            // This will push null onto the stack when run and the local variable on the lhs of the equality operator will be set to null
            Value nullValue = new Value(null);
            nullValue.Compile(parent, token, tokens, lines);
        }
    }
}