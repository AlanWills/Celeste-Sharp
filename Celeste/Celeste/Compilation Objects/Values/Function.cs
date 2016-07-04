﻿using System;
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
        internal Scope FunctionScope { get; set; }

        internal List<string> ParameterNames { get; set; }

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
                Reference funcRef = new Reference(this);
                funcRef.Compile(parent, Name, tokens, lines);
            }
            else
            {
                if (ParameterNames.Count > 0)
                {
                    // Group our inputs under this object so that we can store them underneath this function
                    CompiledStatement thisCallsParams = new CompiledStatement();
                    Add(thisCallsParams);

                    int parameterStartDelimiterIndex = token.IndexOf(FunctionKeyword.parameterStartDelimiter);
                    string inputParameters = token.Substring(parameterStartDelimiterIndex + 1, token.Length - parameterStartDelimiterIndex - 2);
                    string[] inputParameterNames = inputParameters.Split(FunctionKeyword.parameterDelimiter);

                    // Then add the actual parameters we have inputted
                    for (int i = inputParameterNames.Length - 1; i >= 0 ; i--)
                    {
                        // Add our parameters in reverse order, so they get added to the stack in reverse order
                        Debug.Assert(CelesteCompiler.CompileToken(inputParameterNames[i], thisCallsParams), "Failed to compile input parameter");
                    }
                }

                // Add a reference to this function in our compile tree after we have added all of the inputs
                base.Compile(parent, token, tokens, lines);
            }

            // Any local variable that is not set will be null
            // Any extra parameters will be added, but because we add them in reverse order, if they are not needed they will just be thrown away on the stack
        }

        public override void PerformOperation()
        {
            // Set up our parameters if required
            if (ParameterNames.Count > 0)
            {
                if (ChildCount > 0)
                {
                    CompiledStatement thisCallsParams = ChildCompiledStatements[0];
                    ChildCompiledStatements.RemoveAt(0);

                    // This will run through each stored parameter and add them to the stack
                    thisCallsParams.PerformOperation();
                }
            }

            // Set up our parameters - read them off the stack with the first parameter at the top
            for (int i = 0; i < ParameterNames.Count; i++)
            {
                Variable functionParameter = FunctionScope.GetLocalVariable(ParameterNames[i], ScopeSearchOption.kThisScope);

                if (CelesteStack.StackSize > 0)
                {
                    CelesteObject input = CelesteStack.Pop();

                    if (input.IsReference())
                    {
                        functionParameter.Value = input.ValueImpl;
                    }
                    else
                    {
                        (functionParameter.Value as Reference).Value = input.Value;
                    }
                }
                else
                {
                    (functionParameter.Value as Reference).Value = null;
                }
            }

            CelesteStack.CurrentScope = FunctionScope;

            // Performs all the operators on the children first
            foreach (CompiledStatement statement in FuncImpl.ChildCompiledStatements)
            {
                statement.PerformOperation();
                if (statement is ReturnKeyword)
                {
                    // Stop iterating through if we have hit a ReturnKeyword in our function
                    break;
                }
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
