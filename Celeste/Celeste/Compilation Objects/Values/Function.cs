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
        /// The scope created for all the scoped variables declared within this function
        /// </summary>
        internal Scope FunctionScope { get; private set; }

        private List<string> ParameterNames { get; set; }

        #endregion

        public Function(string name) :
            base(name)
        {
            SetReferencedValue(new CompiledStatement());

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
                if (ParameterNames.Count > 0)
                {
                    int parameterStartDelimiterIndex = token.IndexOf(FunctionKeyword.parameterStartDelimiter);
                    string inputParameters = token.Substring(parameterStartDelimiterIndex + 1, token.Length - parameterStartDelimiterIndex - 2);
                    string[] inputParameterNames = inputParameters.Split(FunctionKeyword.parameterDelimiter);

                    for (int i = 0, n = inputParameterNames.Length; i < Math.Min(n, ParameterNames.Count); i++)
                    {
                        Debug.Assert(CelesteCompiler.CompileToken(inputParameterNames[i], parent), "Failed to compile input parameter");
                    }

                    // Fill out the rest of our parameters with null if we have not passed in enough
                    for (int i = inputParameterNames.Length; i < ParameterNames.Count; i++)
                    {
                        // This will push null onto the stack for every parameter we have not provided an input for
                        Reference refToNull = new Reference(null);
                        refToNull.Compile(parent, "null", tokens, lines);
                    }
                }

                // Add a reference to this function in our compile tree after we have added all of the inputs
                base.Compile(parent, token, tokens, lines);
            }

            // Any local variable that is not set will be null
            // Any extra parameters will be ignored
        }

        public override void PerformOperation()
        {
            // Set up our parameters - read them off the stack in reverse order
            for (int i = ParameterNames.Count - 1; i >= 0; i--)
            {
                Debug.Assert(CelesteStack.StackSize > 0, "Insufficient parameters to function");
                CelesteObject input = CelesteStack.Pop();

                Variable functionParameter = FunctionScope.GetLocalVariable(ParameterNames[i], ScopeSearchOption.kThisScope);

                if (input.IsReference())
                {
                    functionParameter.Value = input.ValueImpl;
                }
                else
                {
                    (functionParameter.Value as Reference).Value = input.Value;
                }
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
