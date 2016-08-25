using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Celeste
{
    internal enum ScopeSearchOption
    {
        kThisScope,         // Search this scope's local variables for a variable
        kUpwardsRecursive   // Search this scope's local variables and then upwards through to the global scope
    }

    /// <summary>
    /// Represents a scope within our language and holds local variables in a scoping hierarchy
    /// </summary>
    internal class Scope : CompiledStatement
    {
        #region Properties and Fields

        /// <summary>
        /// Our scopes will all be nested inside each other and we need to store them in a tree for variable look up.
        /// This reference to a parent scope will allow us to create this structure
        /// </summary>
        internal Scope ParentScope { get; private set; }

        /// <summary>
        /// A list of the variables contained within this scope.
        /// Key = name of variable
        /// </summary>
        protected Dictionary<string, Variable> LocalVariables { get; private set; }

        /// <summary>
        /// The number of local variables and functions defined in this scope
        /// </summary>
        internal int VariableCount { get { return LocalVariables.Count; } }

        public string Name { private get; set; }

        #endregion

        public Scope(string name = "") :
            this(CelesteStack.CurrentScope, name)
        {
            
        }

        public Scope(Scope parentScope, string name = "")
        {
            Name = name;
            LocalVariables = new Dictionary<string, Variable>();

            // Add the scope to the scoping tree by setting the parent and adding to our list
            // For the global scope, the current scope will be null
            ParentScope = parentScope;
            CelesteStack.Scopes.Add(this);
            CelesteStack.CurrentScope = this;
        }

        #region Utility Functions

        /// <summary>
        /// Searches through this scopes local variables for the inputted variable.
        /// If specified, this search will continue recursively up the nesting tree until we reach global scope.
        /// </summary>
        /// <param name="variableName"></param>
        /// <returns></returns>
        internal bool VariableExists(string variableName, ScopeSearchOption searchOption = ScopeSearchOption.kThisScope)
        {
            bool result = LocalVariables.ContainsKey(variableName);
            
            if (result || searchOption == ScopeSearchOption.kThisScope || ParentScope == null)
            {
                return result;
            }

            // Search the parent - this will recurse upwards
            return ParentScope.VariableExists(variableName, searchOption);
        }

        /// <summary>
        /// Creates an instance of the inputted Variable type an adds it to the LocalVariables
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="variableName"></param>
        /// <returns></returns>
        internal T CreateLocalVariable<T>(string variableName) where T : Variable
        {
            Debug.Assert(!VariableExists(variableName));

            T variable = (T)Activator.CreateInstance(typeof(T), new object[1] { variableName });
            LocalVariables.Add(variableName, variable);

            return variable;
        }

        /// <summary>
        /// Adds the inputted variable to the LocalVariables
        /// </summary>
        /// <param name="variable"></param>
        internal void AddLocalVariable(Variable variable)
        {
            Debug.Assert(!VariableExists(variable.Name));
            LocalVariables.Add(variable.Name, variable);
        }

        internal Variable GetLocalVariable(string variableName, ScopeSearchOption searchOption = ScopeSearchOption.kUpwardsRecursive)
        {
            Debug.Assert(VariableExists(variableName, searchOption));

            Variable variable;
            if (LocalVariables.TryGetValue(variableName, out variable))
            {
                return variable;
            }
            else if (ParentScope != null)
            {
                return ParentScope.GetLocalVariable(variableName);
            }
            else
            {
                return null;
            }
        }

        internal Variable RemoveLocalVariable(string variableName)
        {
            Debug.Assert(VariableExists(variableName));

            Variable variable = LocalVariables[variableName];
            LocalVariables.Remove(variableName);

            return variable;
        }
    }

    #endregion
}