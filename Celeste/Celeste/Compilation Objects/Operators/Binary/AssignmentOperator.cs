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

        public static bool IsAssignmentOperator(string token)
        {
            return token == scriptToken;
        }

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
            // All functions are references so check this condition first
            if (rhs.IsFunction())
            {
                Debug.Assert(lhs.IsFunction(), "A function can only be assigned to another function");
                Function lhsFunc = lhs.AsFunction();
                Function rhsFunc = rhs.AsFunction();

                Debug.Assert(CelesteStack.CurrentScope != lhsFunc.FunctionScope, "Cannot reassign functions inside their own scope");
                lhsFunc.FunctionScope = rhsFunc.FunctionScope;
                lhsFunc.ParameterNames = rhsFunc.ParameterNames;
                lhsFunc.Ref = rhsFunc.Ref;  // This is equivalent to setting their implementation to be the same
            }
            else if (rhs.IsReference())
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