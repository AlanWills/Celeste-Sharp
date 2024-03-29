﻿using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// An operator which returns:
    /// 1) The logical opposite of a Value bool
    /// 2) If the Value is null also returns true
    /// 2) The opposite of the expression Reference != null
    /// </summary>
    internal class NotOperator : UnaryOperator
    {
        internal static string scriptToken = "!";

        public static bool IsNotOperator(string token)
        {
            // The unary operator nees to be right next to another token to work - there can be no spaces
            return token.StartsWith(scriptToken) && token.Length > 1;
        }

        #region Virtual Functions

        /// <summary>
        /// Removes the the object at the top of the stack and performs the not operation.
        /// Then, pushes the result of the equality on to the top of the stack.
        /// </summary>
        public override void PerformOperation()
        {
            base.PerformOperation();

            // If we have fewer than 1 object on the stack we cannot perform this operation
            Debug.Assert(CelesteStack.StackSize >= 1, "Not enough elements on the stack for the '!' operator");

            CelesteObject rhs = CelesteStack.Pop();

            // The stack will wrap our result in a CelesteObject, so just push the actual result of the equality
            bool result = false;
            Reference rhsRef = rhs.AsReference();

            // Check to see whether our rhs is a reference
            if (rhsRef != null)
            {
                // Returns the result of the reference being equal to null
                result = rhsRef.Value == null;
            }
            else
            {
                // Else returns the logical opposite of the bool value
                result = (rhs.Value == null) || ((rhs.Value is bool) && !(bool)rhs.Value);
            }

            // We then finally push the result of the equality test onto the stack
            CelesteStack.Push(result);
        }

        #endregion
    }
}
