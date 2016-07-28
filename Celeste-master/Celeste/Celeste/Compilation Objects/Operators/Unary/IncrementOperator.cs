using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// An operator which will increase a number or variabe holding a number by 1.
    /// Can be applied before or after the reference to the varable is compiled which will cause it to act first if before,
    /// and after if after.  
    /// If writing 'scoped var = ++var2', this is equal to incrementing var2 and then assigning to var.
    /// If writing 'scoped var = var2++', this is equal to assinging to var2 and then incrementing var.
    /// </summary>
    internal class IncrementOperator : UnaryOperator
    {
        internal static string scriptToken = "++";

        public static bool IsIncrementOperator(string token)
        {
            return token.StartsWith(scriptToken) || token.EndsWith(scriptToken);
        }

        #region Virtual Functions

        // When we compile we need to check which end the increment is on and then check for a variable name accordingly

        /// <summary>
        /// Pops the first element on the top of the stack and checks to see if it is a reference to a number.
        /// If it is, it increments the number and then pushes the object back onto the stack.
        /// </summary>
        public override void PerformOperation()
        {
            base.PerformOperation();

            Debug.Assert(CelesteStack.StackSize > 0, "Must be an object on the stack for the increment operator");

            CelesteObject rhs = CelesteStack.Pop();
            if (rhs.IsNumber())
            {
                float newFloat = rhs.As<float>();
                newFloat++;
                rhs.Value = newFloat;
            }
            else
            {
                Debug.Fail("Increment operator can only be applied to a variable with a numerical value");
            }

            CelesteStack.Push(rhs);
        }

        #endregion
    }
}