using System.Collections.Generic;
using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// The equality operator which tests whether two Values are equal for value types and reference for reference types.
    /// </summary>
    internal class EqualityOperator : BinaryOperator
    {
        internal static string scriptToken = "==";

        #region Virtual Functions

        /// <summary>
        /// Equality should take precedence over assignment
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="token"></param>
        /// <param name="tokens"></param>
        /// <param name="lines"></param>
        public override void Compile(CompiledStatement parent, string token, LinkedList<string> tokens, LinkedList<string> lines)
        {
            base.Compile(parent, token, tokens, lines);

            // If we have a child assignment operator (it can only be in the first position, we swap it with this operator - this should act first
            // This means we have an expression of the form:    A = B == C
            if (ChildCompiledStatements[0] is AssignmentOperator)
            {
                SwapWithChildBinaryOperator(parent);
            }
        }

        /// <summary>
        /// Removes the two objects at the top of the stack and equates them.
        /// Then, pushes the result of the equality on to the top of the stack.
        /// </summary>
        public override void PerformOperation()
        {
            base.PerformOperation();

            // If we have fewer than 2 objects on the stack we cannot perform this operation
            Debug.Assert(CelesteStack.StackSize >= 2, "Not enough elements on the stack for the equality operator");

            CelesteObject rhs = CelesteStack.Pop();
            CelesteObject lhs = CelesteStack.Pop();

            // The stack will wrap our result in a CelesteObject, so just push the actual result of the equality
            bool result = false;
            Reference lhsRef = lhs.Value as Reference;
            Reference rhsRef = rhs.Value as Reference;

            // Check to see whether our lhs is a reference
            if (lhsRef != null)
            {
                if (rhsRef != null)
                {
                    // If the rhs is a reference too, we compare references
                    result = lhsRef == rhsRef;
                }
                else
                {
                    // Otherwise we compare the value of the rhs with the value of the lhs' referenced object
                    result = rhs.Value.ValueEquals(lhsRef.Value);
                }
            }
            else
            {
                if (rhsRef != null)
                {
                    // If the rhs is a reference, we compare the value of the rhs' referenced object with the value of the lhs
                    result = lhs.Value.ValueEquals(rhsRef.Value);
                }
                else
                {
                    // Otherwise we compare the value of the rhs with the value of the lhs - they are both values
                    result = lhs.Value.ValueEquals(rhs.Value);
                }
            }
            
            // We then finally push the result of the equality test onto the stack
            CelesteStack.Push(result);
        }

        #endregion
    }
}