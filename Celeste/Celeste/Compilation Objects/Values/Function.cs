﻿namespace Celeste
{
    /// <summary>
    /// Stores a Compiled Statement tree in it's stored value.
    /// This can be changed to change the behaviour of the function.
    /// </summary>
    internal class Function : Variable
    {
        #region Properties and Fields

        public CompiledStatement FuncImpl { get { return ((_Value as Reference).Value as CompiledStatement); } }

        /// <summary>
        /// The scope created for all the scoped variables declared within this function
        /// </summary>
        internal Scope FunctionScope { get; private set; }

        private string _name = "";
        public string Name
        {
            set
            {
                _name = value;
                FunctionScope.Name = _name + "Scope";
            }
        }

        #endregion

        public Function() :
            this("")
        {
        }

        public Function(string name) :
            base()
        {
            (_Value as Reference).Value = new CompiledStatement();

            FunctionScope = new Scope();
            _name = name;
        }

        #region Virtual Functions

        public override void PerformOperation()
        {
            CelesteStack.CurrentScope = FunctionScope;

            // Performs all the operators on the children first
            foreach (CompiledStatement statement in FuncImpl.ChildCompiledStatements)
            {
                statement.PerformOperation();
            }

            CelesteStack.Scopes.Remove(FunctionScope);
            CelesteStack.CurrentScope = FunctionScope.ParentScope;
        }

        #endregion
    }
}
