using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// The multiply operator
    /// </summary>
    internal class MultiplyOperator : BinaryOperator
    {
        #region Virtual Functions

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
