using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// The equality operator - assigns a value to variable
    /// </summary>
    internal class EqualityOperator : BinaryOperator
    {
        /// <summary>
        /// Assigns the expression on the right to the local variable on the left
        /// </summary>
        public override void PerformOperation()
        {
            base.PerformOperation();

            // If we have fewer than 2 objects on the stack we cannot perform this operator
            Debug.Assert(CelesteStack.StackSize >= 2, "Not enough elements on the stack for equality operator");

            CelesteObject rhs = CelesteStack.Pop();
            CelesteObject lhs = CelesteStack.Pop();

            lhs.Value = rhs.Value;
        }
    }
}
