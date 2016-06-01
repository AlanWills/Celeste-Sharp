using System.Collections.Generic;
using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// The division operator
    /// </summary>
    internal class DivideOperator : BinaryOperator
    {
        #region Virtual Functions

        /// <summary>
        /// Division should take precedence over Assignment
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="token"></param>
        /// <param name="tokens"></param>
        /// <param name="lines"></param>
        public override void Compile(CompiledStatement parent, string token, LinkedList<string> tokens, LinkedList<string> lines)
        {
            base.Compile(parent, token, tokens, lines);

            if (ChildCompiledStatements[0] is AssignmentOperator)
            {
                SwapWithChildBinaryOperator(parent);
            }
        }

        /// <summary>
        /// Removes the two objects at the top of the stack and divides them together.
        /// Then, pushes the result on to the top of the stack.
        /// </summary>
        public override void PerformOperation()
        {
            base.PerformOperation();

            // If we have fewer than 2 objects on the stack we cannot do this
            Debug.Assert(CelesteStack.StackSize >= 2, "Not enough elements on the stack for divide operator");

            CelesteObject rhs = CelesteStack.Pop();
            CelesteObject lhs = CelesteStack.Pop();

            // The stack will wrap our division result in a CelesteObject, so just push the actual value of the division
            if (lhs.IsNumber() && rhs.IsNumber())
            {
                float rhsAsFloat = rhs.As<float>();
                if (rhsAsFloat != 0)
                {
                    CelesteStack.Push(lhs.As<float>() / rhsAsFloat);
                }
                else
                {
                    Debug.Fail("Division by zero");
                }
            }
            else
            {
                Debug.Fail("Invalid parameters to add operation.");
            }
        }

        #endregion
    }
}
