using System;
using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// The or operator, which tests whether at least one of the two objects it acts upon is:
    /// 1) true for Values
    /// 2) != null for References
    /// </summary>
    internal class OrOperator : LogicalBinaryOperator
    {
        internal static string scriptToken = "||";

        #region Virtual Functions

        /// <summary>
        /// Returns whether either one of the references is not null.
        /// </summary>
        /// <param name="lhsRef"></param>
        /// <param name="rhsRef"></param>
        /// <returns></returns>
        protected override bool ReferenceReferenceOperation(Reference lhsRef, Reference rhsRef)
        {
            return lhsRef.Value != null || rhsRef.Value != null;
        }

        /// <summary>
        /// Returns whether the reference is null or the value is true.
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool ReferenceValueOperation(Reference reference, object value)
        {
            return reference.Value != null || (value is bool && (bool)value);
        }

        /// <summary>
        /// Returns whether one of the two values is 'true'.
        /// </summary>
        /// <param name="lhsVal"></param>
        /// <param name="rhsVal"></param>
        /// <returns></returns>
        protected override bool ValueValueOperation(object lhsVal, object rhsVal)
        {
            return (lhsVal is bool && (bool)lhsVal) || (rhsVal is bool && (bool)rhsVal);
        }

        #endregion
    }
}
