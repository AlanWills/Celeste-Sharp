using System.Collections.Generic;
using System.Diagnostics;

namespace Celeste
{
    internal enum CompileType
    {
        kValue,
        kOperator,
    }

    /// <summary>
    /// A class representing a node in our compile tree which can have children objects.
    /// Do not want to expose this outside of the namespace with the compiler.
    /// </summary>
    internal class CompiledStatement
    {
        #region Properties and Fields

        /// <summary>
        /// The direct children of this statement in our compilation tree
        /// </summary>
        private List<CompiledStatement> childCompiledStatements = new List<CompiledStatement>();
        public List<CompiledStatement> ChildCompiledStatements
        {
            get { return childCompiledStatements; }
        }

        /// <summary>
        /// The number of statements directly parented underneath this
        /// </summary>
        public int ChildCount { get { return ChildCompiledStatements.Count; } }

        #endregion

        #region Virtual Functions

        /// <summary>
        /// Attempt to compile the inputted token as this object
        /// </summary>
        /// <param name="rootStatement"></param>
        /// <param name="strings"></param>
        public virtual void Compile(CompiledStatement parent, string token, LinkedList<string> tokens, LinkedList<string> lines) { }

        /// <summary>
        /// The function which will be overriden in inherited operator classes which acts on our stack.
        /// </summary>
        public virtual void PerformOperation()
        {
            // Performs all the operators on the children first
            foreach (CompiledStatement statement in ChildCompiledStatements)
            {
                statement.PerformOperation();
            }
        }

        #endregion

        #region Utility Functions

        /// <summary>
        /// A wrapper around the add function for our child list
        /// </summary>
        /// <param name="compiledStatement"></param>
        public void Add(CompiledStatement compiledStatement)
        {
            ChildCompiledStatements.Add(compiledStatement);
        }

        /// <summary>
        /// Removes a child from this compile statement's children and returns it for adding somewhere else
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public CompiledStatement RemoveAt(int index)
        {
            Debug.Assert(ChildCompiledStatements.Count > index);
            CompiledStatement child = ChildCompiledStatements[index];
            ChildCompiledStatements.RemoveAt(index);

            return child;
        }

        /// <summary>
        /// Removes a child at an index in from this compiled statement and moves it into the inputted newParent
        /// </summary>
        /// <param name="index"></param>
        /// <param name="newParent"></param>
        public void MoveChildAtIndex(int index, CompiledStatement newParent)
        {
            newParent.Add(RemoveAt(index));
        }

        #endregion
    }
}