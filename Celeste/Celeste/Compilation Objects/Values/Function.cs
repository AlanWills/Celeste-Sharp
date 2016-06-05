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
        /// When the function is called, the first child of this list will be run to set up the parameters for that call.
        /// Once this has been done it will be removed from the list.
        /// </summary>
        private List<CompiledStatement> ParameterImpl { get; set; }

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
            ParameterImpl = new List<CompiledStatement>();

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
            // If we have no brackets, we are trying to compile the function as a value rather than as a call (for use in equality for example)
            if (token.IndexOf(FunctionKeyword.parameterStartDelimiter) < 0)
            {
                CompiledStatement funcStatement = null;
                if (parent.ChildCompiledStatements[parent.ChildCount - 1] is AssignmentOperator)
                {
                    // This is being compiled on the rhs of an assignment so we push the value of the FuncImpl
                    funcStatement = new Value(FuncImpl);
                }
                else
                {
                    // Wrap the reference to our FuncImpl so that we can affect it by assignment
                    funcStatement = new Reference(Value);
                }

                funcStatement.Compile(parent, token, tokens, lines);
            }
            else
            {
                // Add a reference to this function in our compile tree
                base.Compile(parent, token, tokens, lines);

                if (ParameterNames.Count > 0)
                {
                    // Only add parameters if our registered parameter names are greater than zero
                    // The compiled statement we will child the input parameters under
                    CompiledStatement thisCallParameters = new CompiledStatement();
                    ParameterImpl.Add(thisCallParameters);

                    int parameterStartDelimiterIndex = token.IndexOf(FunctionKeyword.parameterStartDelimiter);
                    string inputParameters = token.Substring(parameterStartDelimiterIndex + 1, token.Length - parameterStartDelimiterIndex - 2);
                    string[] inputParameterNames = inputParameters.Split(FunctionKeyword.parameterDelimiter);

                    for (int i = 0, n = inputParameterNames.Length; i < Math.Min(n, ParameterNames.Count); i++)
                    {
                        CelesteCompiler.CompileToken(inputParameterNames[i], thisCallParameters);
                        Debug.Assert(thisCallParameters.ChildCompiledStatements[i] is Value || thisCallParameters.ChildCompiledStatements[i] is Variable);
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
                // Set up our parameters
                for (int i = 0; i < ParameterNames.Count; i++)
                {
                    Variable functionParameter = FunctionScope.GetLocalVariable(ParameterNames[i], ScopeSearchOption.kThisScope);

                    if (i < ParameterImpl[0].ChildCount)
                    {
                        if (ParameterImpl[0].ChildCompiledStatements[i] is Variable)
                        {
                            // Set the references to be the same so that modifications to the function's local variable affect the variable we passed in
                            functionParameter.Value = (ParameterImpl[0].ChildCompiledStatements[i] as Variable).Value;
                        }
                        else if (ParameterImpl[0].ChildCompiledStatements[i] is Value)
                        {
                            // Otherwise we have passed in a value
                            functionParameter.Ref.Value = ((ParameterImpl[0].ChildCompiledStatements[i] as Value)._Value);
                        }
                        else
                        {
                            Debug.Fail("Invalid input value");
                        }
                    }
                    else
                    {
                        // Clear our reference to any input variable ready for a new call
                        functionParameter.Ref = null;
                    }
                }

                ParameterImpl.RemoveAt(0);
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
