namespace Celeste
{
    /// <summary>
    /// The non equality operator which tests whether two Values are not equal for value types and references not equal for reference types
    /// </summary>
    internal class NonEqualityOperator : LogicalBinaryOperator
    {
        private static string scriptToken = "!=";

        #region Virtual Functions

        public static bool IsNonEqualityOperator(string token)
        {
            return token.StartsWith(scriptToken);
        }

        /// <summary>
        /// Equate the references
        /// </summary>
        /// <param name="lhsRef"></param>
        /// <param name="rhsRef"></param>
        /// <returns></returns>
        protected override bool ReferenceReferenceOperation(Reference lhsRef, Reference rhsRef)
        {
            return lhsRef != rhsRef;
        }

        /// <summary>
        /// Returns true if the value of the referenced object and the value are not equal
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool ReferenceValueOperation(Reference reference, object value)
        {
            return !value.ValueEquals(reference.Value);
        }

        /// <summary>
        /// Returns true if the two values are not equal
        /// </summary>
        /// <param name="lhsVal"></param>
        /// <param name="rhsVal"></param>
        /// <returns></returns>
        protected override bool ValueValueOperation(object lhsVal, object rhsVal)
        {
            return !lhsVal.ValueEquals(rhsVal);
        }

        #endregion
    }
}
