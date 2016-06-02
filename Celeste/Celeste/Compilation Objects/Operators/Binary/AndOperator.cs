using System;
using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// Represents the 'binary' logical operator which checks for true for Values and equates to null for References
    /// </summary>
    internal class AndOperator : LogicalBinaryOperator
    {
        internal static string scriptToken = "&&";

        #region Virtual Functions

        /// <summary>
        /// Check to see whether the two references are null.
        /// Returns true only if they are both not null.
        /// </summary>
        /// <param name="lhsRef"></param>
        /// <param name="rhsRef"></param>
        /// <returns></returns>
        protected override bool ReferenceReferenceOperation(Reference lhsRef, Reference rhsRef)
        {
            return lhsRef.Value != null && rhsRef.Value != null;
        }

        /// <summary>
        /// Checks whether the reference is null and whether the value is true.
        /// Returns true only if this is the case
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool ReferenceValueOperation(Reference reference, object value)
        {
            // We check whether the reference is not null first, before checking the value
            // Do it in this order so we do not have to unbox the value unless necessary
            return reference.Value != null && (value is bool && (bool)value);
        }

        /// <summary>
        /// Checks whether both values are true.
        /// Only returns true if this is the case.
        /// </summary>
        /// <param name="lhsVal"></param>
        /// <param name="rhsVal"></param>
        /// <returns></returns>
        protected override bool ValueValueOperation(object lhsVal, object rhsVal)
        {
            return (lhsVal is bool && (bool)lhsVal) && (rhsVal is bool && (bool)rhsVal);
        }

        #endregion
    }
}
