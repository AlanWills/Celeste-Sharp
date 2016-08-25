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

        /// <summary>
        /// The reference to the value object this variable represents
        /// </summary>
        public Value Val
        {
            get { return (Value as Reference).Value as Value; }
            set { (Value as Reference).Value = value; }
        }

        /// <summary>
        /// Every variable has two layers of referencing, but the first layer is subtle and below the surface - this allows us to change what the inner reference is referencing
        /// and have the change call through to all other objects referencing it.
        /// The outer hidder reference is a reference either to a Reference object or a Value object.
        /// These bools indicate which it is.
        /// </summary>
        public bool IsValueType { get { return (Value as Reference).Value is Value; } }
        public bool IsReferenceType { get { return (Value as Reference).Value is Reference; } }

        internal static string variableDelimiter = ",";

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

        /// <summary>
        /// Variables are just layers of referencing so we really want to ToString the base value being referenced
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Value.ToString();
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