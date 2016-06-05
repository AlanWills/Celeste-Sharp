using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// Stores a Compiled Statement tree in it's stored value.
    /// This can be changed to change the behaviour of the function.
    /// </summary>
    internal class Function : Variable
    {
        #region Properties and Fields

        public CompiledStatement FuncImpl { get { return Ref.Value as CompiledStatement; } }

        /// <summary>
        /// A list where each child in the list represents one round of parameters to the function.
        /// When the function is called, the first child of this list will be iterated over and the parameters set to the values that exist there.
        /// Once this has been done it will be removed from the list.
        /// </summary>
        private List<List<object>> ParameterImpl { get; set; }

        /// <summary>
        /// The scope created for all the scoped variables declared within this function
        /// </summary>
        internal Scope FunctionScope { get; private set; }

        private List<string> ParameterNames { get; set; }

        #endregion

        public Function(string name) :
            base(name)
        {
            SetReferencedValue(new CompiledStatement());
            ParameterImpl = new List<List<object>>();

            FunctionScope = new Scope();
            FunctionScope.Name = Name + "Scope";

            ParameterNames = new List<string>();
        }

        #region Virtual Functions

        /// <summary>
        /// Call compile when you wish to implement the function.
        /// Process the actual variable names that have been passed into the function using the token.
        /// Formatting will be func(param1, param2, ...).
        /// Create a variable underneath this for each variable we are passing in.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="token"></param>
        /// <param name="tokens"></param>
        /// <param name="lines"></param>
        public override void Compile(CompiledStatement parent, string token, LinkedList<string> tokens, LinkedList<string> lines)
        {
            // If we have no brackets, we are trying to compile the function as a reference rather than as a call (for use in equality for example)
            if (token.IndexOf(FunctionKeyword.parameterStartDelimiter) < 0)
            {
                (Value as Reference).Compile(parent, token, tokens, lines);
            }
            else
            {
                // Add a reference to this function in our compile tree
                base.Compile(parent, token, tokens, lines);

                if (ParameterNames.Count > 0)
                {
                    // Only add parameters if our registered parameter names are greater than zero
                    List<object> thisCallParameters = new List<object>(ParameterNames.Count);
                    ParameterImpl.Add(thisCallParameters);

                    int parameterStartDelimiterIndex = token.IndexOf(FunctionKeyword.parameterStartDelimiter);
                    string inputParameters = token.Substring(parameterStartDelimiterIndex + 1, token.Length - parameterStartDelimiterIndex - 2);
                    string[] inputParameterNames = inputParameters.Split(FunctionKeyword.parameterDelimiter);

                    for (int i = 0, n = inputParameterNames.Length; i < Math.Min(n, ParameterNames.Count); i++)
                    {
                        Debug.Assert(CelesteCompiler.CompileToken(inputParameterNames[i], this), "Failed to compile input parameter");
                        CompiledStatement input = ChildCompiledStatements[ChildCount - 1];
                        ChildCompiledStatements.RemoveAt(ChildCount - 1);

                        if (input is Value)
                        {
                            thisCallParameters.Add(new Reference((input as Value)._Value));
                        }
                        else if (input is Variable)
                        {
                            thisCallParameters.Add((input as Variable).Value);
                        }
                        else
                        {
                            Debug.Fail("Invalid input to function");
                        }
                    }

                    // Fill out the rest of our parameters with null if we have not passed in enough
                    for (int i = inputParameterNames.Length; i < thisCallParameters.Capacity; i++)
                    {
                        thisCallParameters.Add(null);
                    }
                }
            }

            // Any local variable that is not set will be null
            // Any extra parameters will be ignored
        }

        public override void PerformOperation()
        {
            if (ParameterImpl.Count > 0)
            {
                List<object> inputParameters = ParameterImpl[0];
                ParameterImpl.RemoveAt(0);

                // Set up our parameters
                for (int i = 0; i < ParameterNames.Count; i++)
                {
                    Variable functionParameter = FunctionScope.GetLocalVariable(ParameterNames[i], ScopeSearchOption.kThisScope);
                    functionParameter.Value = inputParameters[i];
                }

                // Ensure our list will be destroyed by GC
                inputParameters.Clear();
            }

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

        #region Utility Functions

        public void SetParameters(string[] parameterNames)
        {
            foreach (string parameterName in parameterNames)
            {
                if (!string.IsNullOrWhiteSpace(parameterName))
                {
                    FunctionScope.AddLocalVariable<Variable>(parameterName);
                    ParameterNames.Add(parameterName);
                }
            }
        }

        #endregion
    }
}
