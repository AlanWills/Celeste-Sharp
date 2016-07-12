using System.Collections.Generic;

namespace Celeste
{
    /// <summary>
    /// Represents a boolean value
    /// </summary>
    [CompilableValue]
    internal class Bool : Value
    {
        /// <summary>
        /// This must remain public for use in the Compiler
        /// </summary>
        public Bool() :
            base(false)
        {

        }

        /// <summary>
        /// Attempt to parse the inputted token as a bool
        /// </summary>
        /// <param name="token"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool IsBool(string token)
        {
            bool result;
            return bool.TryParse(token, out result);
        }

        #region Virtual Functions

        public override void Compile(CompiledStatement parent, string token, LinkedList<string> tokens, LinkedList<string> lines)
        {
            base.Compile(parent, token, tokens, lines);

            bool result;
            bool.TryParse(token, out result);
            _Value = result;
        }

        #endregion
    }
}
