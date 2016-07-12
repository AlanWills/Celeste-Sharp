using System.Collections.Generic;
using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// An abstract class which wraps up the logic of validating and implementing a logical binary operator.
    /// </summary>
    internal abstract class LogicalBinaryOperator : BinaryOperator
    {
        #region Virtual Functions

        /// <summary>
        /// Logic should take precedence over assignment
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="token"></param>
        /// <param name="tokens"></param>
        /// <param name="lines"></param>
        public sealed override void Compile(CompiledStatement parent, string token, LinkedList<string> tokens, LinkedList<string> lines)
        {
            base.Compile(parent, token, tokens, lines);

            // If we have a child assignment operator (it can only be in the first position, we swap it with this operator - this should act first
            // This means we have an expression of the form:    A = B logic C
            if (ChildCompiledStatements[0] is AssignmentOperator)
            {
                SwapWithChildBinaryOperator(parent);
            }
        }

        /// <summary>
        /// Specifies the behaviour this operator will have on two references
        /// </summary>
        /// <param name="lhsRef"></param>
        /// <param name="rhsRef"></param>
        /// <returns></returns>
        protected abstract bool ReferenceReferenceOperation(Reference lhsRef, Reference rhsRef);

        /// <summary>
        /// Specifies the behaviour this operator will have on a reference and a value
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected abstract bool ReferenceValueOperation(Reference reference, object value);

        /// <summary>
        /// Specifies the behaviour this operator will have on two values
        /// </summary>
        /// <param name="lhsVal"></param>
        /// <param name="rhsVal"></param>
        /// <returns></returns>
        protected abstract bool ValueValueOperation(object lhsVal, object rhsVal);

        /// <summary>
        /// Removes the two objects at the top of the stack and performs logical operations them.
        /// Then, pushes the result of the operation on to the top of the stack.
        /// </summary>
        public sealed override void PerformOperation()
        {
            base.PerformOperation();

            // If we have fewer than 2 objects on the stack we cannot perform this operation
            Debug.Assert(CelesteStack.StackSize >= 2, "Not enough elements on the stack for the equality operator");

            CelesteObject rhs = CelesteStack.Pop();
            CelesteObject lhs = CelesteStack.Pop();

            // The stack will wrap our result in a CelesteObject, so just push the actual result of the operation
            bool result = false;
            Reference lhsRef = lhs.AsReference();
            Reference rhsRef = rhs.AsReference();

            // Check to see whether our lhs is a reference
            if (lhsRef != null)
            {
                if (rhsRef != null)
                {
                    // If the rhs is a reference too, we compare references
                    result = ReferenceReferenceOperation(lhsRef, rhsRef);
                }
                else
                {
                    // Otherwise we compare the value of the rhs with the value of the lhs' referenced object
                    result = ReferenceValueOperation(lhsRef, rhs.Value);
                }
            }
            else
            {
                if (rhsRef != null)
                {
                    // If the rhs is a reference, we compare the value of the rhs' referenced object with the value of the lhs
                    result = ReferenceValueOperation(rhsRef, lhs.Value);
                }
                else
                {
                    // Otherwise we compare the value of the rhs with the value of the lhs - they are both values
                    result = ValueValueOperation(lhs.Value, rhs.Value);
                }
            }

            // We then finally push the result of the equality test onto the stack
            CelesteStack.Push(result);
        }

        #endregion
    }
}
