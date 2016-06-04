using System.Collections.Generic;
using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// The multiply operator
    /// </summary>
    internal class MultiplyOperator : BinaryOperator
    {
        private static string scriptToken = "*";

        #region Virtual Functions

        public static bool IsMultiplyOperator(string token)
        {
            return token.StartsWith(scriptToken);
        }

        /// <summary>
        /// Multiplication should take precendence over assignment
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
        /// Removes the two objects at the top of the stack and multiplies them together.
        /// Then, pushes the result on to the top of the stack.
        /// </summary>
        public override void PerformOperation()
        {
            base.PerformOperation();

            // If we have fewer than 2 objects on the stack we are el-bonerino-ed
            Debug.Assert(CelesteStack.StackSize >= 2, "Not enough elements on the stack for multiply operator");

            CelesteObject rhs = CelesteStack.Pop();
            CelesteObject lhs = CelesteStack.Pop();

            // The stack will wrap our multiplication result in a CelesteObject, so just push the actual value of the multiplication
            if (lhs.IsNumber() && rhs.IsNumber())
            {
                CelesteStack.Push(lhs.As<float>() * rhs.As<float>());
            }
            else
            {
                Debug.Fail("Invalid parameters to add operation.");
            }
        }

        #endregion
    }
}
