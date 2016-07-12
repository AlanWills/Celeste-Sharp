using System.Reflection;

namespace Celeste
{
    /// <summary>
    /// A specific implementation of a generic function.
    /// Used to register C# callbacks to script functions via a token.
    /// Has all the properties of a function, so we can reassign the FuncImpl, but the default FuncImpl is just an Invocation.
    /// In the PerformOperation function we invoke the C# generic delegate we have passed to this.
    /// </summary>
    internal class Delegate : Function
    {
        #region Properties and Fields

        /// <summary>
        /// The method info for the function we will callback to when we call PerformOperation on this Delegate.
        /// </summary>
        private MethodInfo Method { get; set; }

        #endregion

        public Delegate(string name, MethodInfo methodInfo) :
            base(name)
        {
            Method = methodInfo;

            ParameterInfo[] parameters = methodInfo.GetParameters();
            string[] parameterNames = new string[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                parameterNames[i] = parameters[i].Name;
            }

            SetParameters(parameterNames);

            // Set up the callback to our Method
            FuncImpl.Add(new Invocation(methodInfo, GetParameters()));
        }
    }
}