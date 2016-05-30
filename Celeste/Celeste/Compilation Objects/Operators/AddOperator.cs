using System.Collections.Generic;
using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// The add operator
    /// </summary>
    internal class AddOperator : BinaryOperator
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
            Debug.Assert(CelesteStack.StackSize >= 2, "Not enough elements on the stack for add operator");

            CelesteObject rhs = CelesteStack.Pop();
            CelesteObject lhs = CelesteStack.Pop();

            // The stack will wrap our addition result in a CelesteObject, so just push the actual value of the addition
            if (lhs.IsNumber() && rhs.IsNumber())
            {
                CelesteStack.Push(lhs.As<float>() + rhs.As<float>());
            }
            else if (lhs.IsString() && rhs.IsString())
            {
                CelesteStack.Push(lhs.As<string>() + rhs.As<string>());
            }
            else if (lhs.IsList() && rhs.IsList())
            {
                lhs.AsList().AddRange(rhs.AsList());
            }
            else if (lhs.IsTable() && rhs.IsTable())
            {

            }
            else
            {
                Debug.Fail("Invalid parameters to add operation.");
            }
        }

        #endregion
    }
}
