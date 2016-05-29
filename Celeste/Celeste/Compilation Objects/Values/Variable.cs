﻿using System.Collections.Generic;

namespace Celeste
{
    /// <summary>
    /// A class to represent a variable within our program
    /// </summary>
    internal class Variable : Value
    {
        // Don't make the constructor less than public - it's need in the CelesteCompiler
        public Variable() :
            base(new Reference(null))
        {

        }

        #region Virtual Functions

        /// <summary>
        /// Sets this variable's Value to be that of the stored local variable in the CelesteStack
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="token"></param>
        /// <param name="tokens"></param>
        public override void Compile(CompiledStatement parent, string token, List<string> tokens)
        {
            base.Compile(parent, token, tokens);

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