namespace Celeste
{
    /// <summary>
    /// A class to represent a variable within our program
    /// </summary>
    internal class Variable : Reference
    {
        #region Properties and Fields

        public string Name { get; private set; }

        /// <summary>
        /// The reference to the object this variable represents
        /// </summary>
        public Reference Ref
        {
            get { return (Value as Reference).Value as Reference; }
            set { (Value as Reference).Value = value; }
        }

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

            (Value as Reference).PerformOperation();
        }

        #endregion

        public T GetReferencedValue<T>()
        {
            return (T)Ref.Value;
        }

        public void SetReferencedValue(object newValue)
        {
            Ref.Value = newValue;
        }
    }
}