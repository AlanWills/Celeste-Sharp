using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// The assignment operator - assigns the reference to a value from one variable to another so they both reference the same value
    /// </summary>
    internal class AssignmentOperator : BinaryOperator
    {
        internal static string scriptToken = "=";

        #region Virtual Functions

        /// <summary>
        /// Assigns the expression on the right to the local variable on the left
        /// </summary>
        public override void PerformOperation()
        {
            base.PerformOperation();

            // If we have fewer than 2 objects on the stack we cannot perform this operator
            Debug.Assert(CelesteStack.StackSize >= 2, "Not enough elements on the stack for equality operator");

            Scope scope = CelesteStack.CurrentScope;

            CelesteObject rhs = CelesteStack.Pop();
            CelesteObject lhs = CelesteStack.Pop();

            Debug.Assert(lhs.IsReference());
            if (rhs.IsReference())
            {
                lhs.Value = rhs.Value;
            }
            else
            {
                lhs.AsReference().Value = rhs.Value;
            }
        }

        #endregion
    }
}