using System.Collections.Generic;
using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// Represents the 'binary' logical operator which checks for true for Values and equates to null for References
    /// </summary>
    internal class AndOperator : BinaryOperator
    {
        internal static string scriptToken = "&&";

        #region Virtual Functions

        /// <summary>
        /// And should take precedence over assignment
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="token"></param>
        /// <param name="tokens"></param>
        /// <param name="lines"></param>
        public override void Compile(CompiledStatement parent, string token, LinkedList<string> tokens, LinkedList<string> lines)
        {
            base.Compile(parent, token, tokens, lines);

            // If we have a child assignment operator (it can only be in the first position), we swap it with this operator - this should act first
            // This means we have an expression of the form:    A = B && C
            if (ChildCompiledStatements[0] is AssignmentOperator)
            {
                SwapWithChildBinaryOperator(parent);
            }
        }

        /// <summary>
        /// Removes the two objects at the top of the stack and 'or's them.
        /// Then, pushes the result of the or on to the top of the stack.
        /// </summary>
        public override void PerformOperation()
        {
            base.PerformOperation();

            // If we have fewer than 2 objects on the stack we cannot perform this operation
            Debug.Assert(CelesteStack.StackSize >= 2, "Not enough elements on the stack for the and operator");

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
                    // If the rhs is a reference too, we check to see if one of the references is null
                    result = lhsRef.Value != null && rhsRef.Value != null;
                }
                else
                {
                    // Otherwise we see if the lhs's reference is null or the rhs is true
                    result = lhsRef.Value != null && (rhs.Value is bool && (bool)rhs.Value);
                }
            }
            else
            {
                if (rhsRef != null)
                {
                    // If the rhs is a reference, we check whether it is not null and check whether the lhs is true
                    // Do it in this order so we do not have to unbox the value unless necessary
                    result = rhsRef.Value != null && (lhs.Value is bool && (bool)lhs.Value);
                }
                else
                {
                    // Otherwise we check to see if one of the values is true
                    result = (lhs.Value is bool && (bool)lhs.Value) && (rhs.Value is bool && (bool)rhs.Value);
                }
            }

            // We then finally push the result of the equality test onto the stack
            CelesteStack.Push(result);
        }

        #endregion
    }
}
