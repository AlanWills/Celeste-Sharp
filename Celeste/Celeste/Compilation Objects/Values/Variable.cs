using System.Collections.Generic;

namespace Celeste
{
    /// <summary>
    /// A class to represent a variable within our program
    /// </summary>
    internal class Variable : Value
    {
        // Don't make the constructor less than public - it's needed in the CelesteCompiler
        public Variable() :
            base(new Reference(null))
        {

        }

        #region Virtual Functions

        /// <summary>
        /// Sets this variable's Value to be that of the stored local variable in the CelesteStack.
        /// This function should be used not when creating a variable, but rather when the variable that has been created has been referenced elsewhere by name
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="token"></param>
        /// <param name="tokens"></param>
        public override void Compile(CompiledStatement parent, string token, LinkedList<string> tokens, LinkedList<string> lines)
        {
            base.Compile(parent, token, tokens, lines);

            _Value = CelesteStack.CurrentScope.GetLocalVariable(token)._Value;
        }

        public override void PerformOperation()
        {
            // Performs all the operators on the children first
            foreach (CompiledStatement statement in ChildCompiledStatements)
            {
                statement.PerformOperation();
            }

            CelesteStack.Push(_Value as Reference);
        }

        #endregion
    }
}