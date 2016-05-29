using System.Collections.Generic;

namespace Celeste
{
    /// <summary>
    /// Represents a number within our program
    /// </summary>
    internal class Number : Value
    {
        /// <summary>
        /// This must remain public for use in the Compiler
        /// </summary>
        public Number() :
            base(0)
        {

        }

        #region Virtual Functions

        public override void Compile(CompiledStatement parent, string token, List<string> tokens)
        {
            base.Compile(parent, token, tokens);

            float result;
            float.TryParse(token, out result);
            _Value = result;
        }

        #endregion
    }
}
