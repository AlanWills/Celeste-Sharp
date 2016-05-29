using System.Collections.Generic;

namespace Celeste
{
    /// <summary>
    /// A class representing a hard coded compiled value in our script
    /// </summary>
    internal class Value : CompiledStatement
    {
        #region Properties and Fields

        /// <summary>
        /// The object we will push onto the stack
        /// </summary>
        internal object _Value { get; set; }

        #endregion

        internal Value(object value)
        {
            _Value = value;
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

            CelesteStack.Push(_Value);
        }

        #endregion
    }
}
