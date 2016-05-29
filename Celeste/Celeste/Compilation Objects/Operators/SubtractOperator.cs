using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// The subtract operator
    /// </summary>
    internal class SubtractOperator : BinaryOperator
    {
        #region Virtual Functions

        /// <summary>
        /// Removes the two objects at the top of the stack and adds them together.
        /// Then, pushes the result on to the top of the stack.
        /// </summary>
        public override void PerformOperation()
        {
            base.PerformOperation();

            // If we have fewer than 2 objects on the stack we are el-bonerino-ed
            Debug.Assert(CelesteStack.StackSize >= 2, "Not enough elements on the stack for subtract operator");

            CelesteObject rhs = CelesteStack.Pop();
            CelesteObject lhs = CelesteStack.Pop();

            // The stack will wrap our subtraction result in a CelesteObject, so just push the actual value of the subtraction
            if (lhs.IsNumber() && rhs.IsNumber())
            {
                CelesteStack.Push(lhs.As<float>() - rhs.As<float>());
            }
            else if (lhs.IsString() && rhs.IsString())
            {
                string lhsAsString = lhs.As<string>();
                string rhsAsString = rhs.As<string>();

                if (lhsAsString.Contains(rhsAsString))
                {
                    // Push the lhs string having removed the rhs string
                    CelesteStack.Push(lhsAsString.Remove(lhsAsString.IndexOf(rhsAsString), rhsAsString.Length));
                }
                else
                {
                    // If our lhs does not contain our rhs, we just push the original unaltered lhs string
                    CelesteStack.Push(lhsAsString);
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
