using System.Collections.Generic;

namespace Celeste
{
    /// <summary>
    /// A reference wrapper around a stored object to allow references to change the value.
    /// Objects wrapped in a reference can be changed directly by assignment etc.
    /// </summary>
    public class Reference : CompiledStatement
    {
        #region Properties and Fields

        /// <summary>
        /// The actual value that is being stored
        /// </summary>
        public object Value { get; set; }

        #endregion

        public Reference(object value)
        {
            Value = value;
        }

        #region Virtual Functions

        public override void Compile(CompiledStatement parent, string token, LinkedList<string> tokens, LinkedList<string> lines)
        {
            base.Compile(parent, token, tokens, lines);

            parent.Add(this);
        }

        /// <summary>
        /// Pushes our stored object onto the stack
        /// </summary>
        public override void PerformOperation()
        {
            base.PerformOperation();

            CelesteStack.Push(this);
        }

        /// <summary>
        /// Since this is just a reference, we really want to ToString the underlying value being referenced
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Value.ToString();
        }

        #endregion
    }
}
