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

        public CompiledStatement FuncImpl { get { return GetReferencedValue<CompiledStatement>(); } }

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
        /// Create a variable underneath this for each variable we are passing in.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="token"></param>
        /// <param name="tokens"></param>
        /// <param name="lines"></param>
        public override void Compile(CompiledStatement parent, string token, LinkedList<string> tokens, LinkedList<string> lines)
        {
            // We have invoked this function because we have parentheses
            if (tokens.Count > 0 && tokens.First.Value == OpenParenthesis.scriptToken)
            {
                // Group our inputs under this object so that we can store them underneath this function
                CompiledStatement thisCallsParams = new CompiledStatement();
                if (ParameterNames.Count > 0)
                {
                    // Only add our parameters if we have any.
                    // Otherwise this will just strip away the start and end parentheses
                    Add(thisCallsParams);
                }

                string openingParenthesis = CelesteCompiler.PopToken();
                Debug.Assert(openingParenthesis == OpenParenthesis.scriptToken, "No opening parenthesis found for function " + Name);

                string parameter = CelesteCompiler.PopToken();

                CompiledStatement tempContainer = new CompiledStatement();
                while (parameter != CloseParenthesis.scriptToken)
                {
                    Debug.Assert(CelesteCompiler.CompileToken(parameter, tempContainer),
                        "Failed to compile input parameter " + parameter);
                    parameter = CelesteCompiler.PopToken();
                }

                // Add null references first for all of the parameters we are missing
                for (int i = tempContainer.ChildCount; i < ParameterNames.Count; i++)
                {
                    // This will push null onto the stack for every parameter we have not provided an input for
                    Reference refToNull = new Reference(null);
                    refToNull.Compile(thisCallsParams, "null", tokens, lines);
                }

                // Then add the actual parameters we have inputted
                for (int i = tempContainer.ChildCount - 1; i >= 0; i--)
                {
                    // Add our parameters in reverse order, so they get added to the stack in reverse order
                    tempContainer.MoveChildAtIndex(i, thisCallsParams);
                }

                // Add a reference to this function in our compile tree after we have added all of the inputs
                base.Compile(parent, token, tokens, lines);
            }
            else
            {
                // If we have no brackets, we are trying to compile the function as a reference rather than as a call (for use in equality for example)
                Reference funcRef = new Reference(this);
                funcRef.Compile(parent, Name, tokens, lines);
            }

            // Any local variable that is not set will be null
            // Any extra parameters will be added, but because we add them in reverse order, if they are not needed they will just be thrown away on the stack
        }

        public override void PerformOperation()
        {
            // Set up our parameters if required
            if (ParameterNames.Count > 0)
            {
                Debug.Assert(ChildCount > 0, "Fatal error in function call - no arguments to process");
                CompiledStatement thisCallsParams = ChildCompiledStatements[0];
                ChildCompiledStatements.RemoveAt(0);

                // This will run through each stored parameter and add them to the stack
                thisCallsParams.PerformOperation();
            }

            // Set up our parameters - read them off the stack with the first parameter at the top
            for (int i = 0; i < ParameterNames.Count; i++)
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
                    FunctionScope.CreateLocalVariable<Variable>(parameterName.Replace(Delimiter.scriptToken, ""));
                    ParameterNames.Add(parameterName);
                }
            }
        }

        /// <summary>
        /// Creates a new array of references to the variables used as parameters, rather than returning a reference.
        /// </summary>
        /// <returns></returns>
        public List<Variable> GetParameters()
        {
            List<Variable> vars = new List<Variable>(ParameterNames.Count);

            foreach (string name in ParameterNames)
            {
                vars.Add(FunctionScope.GetLocalVariable(name, ScopeSearchOption.kThisScope));
            }

            return vars;
        }

        #endregion
    }
}
