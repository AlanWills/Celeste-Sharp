namespace Celeste
{
    /// <summary>
    /// The equality operator which tests whether two Values are equal for value types and reference for reference types.
    /// </summary>
    internal class EqualityOperator : LogicalBinaryOperator
    {
        internal static string scriptToken = "==";

        #region Virtual Functions

        /// <summary>
        /// Equate the two references
        /// </summary>
        /// <param name="lhsRef"></param>
        /// <param name="rhsRef"></param>
        /// <returns></returns>
        protected override bool ReferenceReferenceOperation(Reference lhsRef, Reference rhsRef)
        {
            return lhsRef == rhsRef;
        }

        /// <summary>
        /// Equate the value of the object the reference points at to the value object
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool ReferenceValueOperation(Reference reference, object value)
        {
            return value.ValueEquals(reference.Value);
        }

        /// <summary>
        /// Equate the two values
        /// </summary>
        /// <param name="lhsVal"></param>
        /// <param name="rhsVal"></param>
        /// <returns></returns>
        protected override bool ValueValueOperation(object lhsVal, object rhsVal)
        {
            return lhsVal.ValueEquals(rhsVal);
        }

        #endregion
    }
}