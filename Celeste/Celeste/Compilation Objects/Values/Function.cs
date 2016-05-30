namespace Celeste
{
    /// <summary>
    /// Stores a Compiled Statement tree in it's stored value.
    /// This can be changed to change the behaviour of the function.
    /// </summary>
    internal class Function : Variable
    {
        #region Properties and Fields

        public CompiledStatement FuncImpl { get { return ((_Value as Reference).Value as CompiledStatement); } }

        #endregion

        public Function() :
            base()
        {
            (_Value as Reference).Value = new CompiledStatement();
        }

        public override void PerformOperation()
        {
            // Performs all the operators on the children first
            foreach (CompiledStatement statement in FuncImpl.ChildCompiledStatements)
            {
                statement.PerformOperation();
            }
        }
    }
}
