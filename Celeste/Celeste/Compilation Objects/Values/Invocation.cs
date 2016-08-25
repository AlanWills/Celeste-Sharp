using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.AccessControl;

namespace Celeste
{
    /// <summary>
    /// Stores C# method data for invoking a C# function at runtime
    /// </summary>
    internal class Invocation : CompiledStatement
    {
        #region Properties and Fields

        /// <summary>
        /// The method we will invoke
        /// </summary>
        private MethodInfo Method { get; set; }

        /// <summary>
        /// References to the parameters in the order they are declared n the function for looking up at runtime.
        /// </summary>
        private List<Variable> OrderedParameterList { get; set; }

        #endregion

        public Invocation(MethodInfo method, List<Variable> orderedParameterList)
        {
            Method = method;
            OrderedParameterList = orderedParameterList;
        }

        #region Virtual Functions

        public override void Compile(CompiledStatement parent, string token, LinkedList<string> tokens, LinkedList<string> lines)
        {
            base.Compile(parent, token, tokens, lines);

            parent.Add(this);
        }

        /// <summary>
        /// Invokes our function by using the Variables we have passed in
        /// </summary>
        public override void PerformOperation()
        {
            base.PerformOperation();

            // Maybe we can improve this - store the array rather than create it every time we invoke the function
            object[] parameters = new object[OrderedParameterList.Count];

            int index = 0;
            foreach (Variable var in OrderedParameterList)
            {
                parameters[index] = new CelesteObject(var.Value as Reference);
                index++;
            }

            Method.Invoke(null, parameters);
        }

        #endregion
    }
}
