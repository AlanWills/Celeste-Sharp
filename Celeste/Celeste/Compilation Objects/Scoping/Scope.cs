using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

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

        #endregion

        public Scope() :
            this(null)
        {
            
        }

        public Scope(Scope parentScope)
        {
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
            
            if (searchOption == ScopeSearchOption.kThisScope || ParentScope == null)
            {
                return result;
            }

            // Search the parent - this will recurse upwards
            return ParentScope.VariableExists(variableName, searchOption);
        }

        internal Variable AddLocalVariable(string variableName)
        {
            Debug.Assert(!VariableExists(variableName));

            Variable variable = new Variable();
            LocalVariables.Add(variableName, variable);

            return variable;
        }

        internal Variable GetLocalVariable(string variableName)
        {
            Debug.Assert(VariableExists(variableName, ScopeSearchOption.kUpwardsRecursive));

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
    }

    #endregion
}