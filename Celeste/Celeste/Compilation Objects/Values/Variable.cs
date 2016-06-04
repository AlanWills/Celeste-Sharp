namespace Celeste
{
    /// <summary>
    /// A class to represent a variable within our program
    /// </summary>
    internal class Variable : Value
    {
        #region Properties and Fields

        public string Name { get; private set; }

        #endregion

        // Don't make the constructor less than public - it's needed in the CelesteCompiler
        public Variable(string variableName) :
            base(new Reference(new Reference(null)))
        {
            Name = variableName;
        }

        #region Virtual Functions

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

        public T GetReferencedValue<T>()
        {
            return (T)((_Value as Reference).Value as Reference).Value;
        }

        public void SetReferencedValue(object newValue)
        {
            ((_Value as Reference).Value as Reference).Value = newValue;
        }
    }
}