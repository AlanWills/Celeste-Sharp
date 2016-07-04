using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestCeleste")]

namespace Celeste
{
    /// <summary>
    /// The heart of our C# VM stack.
    /// </summary>
    public static class CelesteStack
    {
        /// <summary>
        /// The main stack for our scripts
        /// </summary>
        private static Stack celStack = new Stack();
        private static Stack CelStack
        {
            get { return celStack; }
        }

        /// <summary>
        /// Represents how many objects are on the stack
        /// </summary>
        public static int StackSize { get { return CelStack.Count; } }

        internal static ScopeManager Scopes = new ScopeManager();

        /// <summary>
        /// The current scope that any local variables we have parsed will be added to
        /// </summary>
        internal static Scope CurrentScope = new Scope(null, "Global");

        /// <summary>
        /// The global variables declared from all scripts
        /// </summary>
        internal static Scope GlobalScope = CurrentScope;

        #region Push Functions

        /// <summary>
        /// Pushes a generic object onto our stack.
        /// Should not be used for lists/arrays etc.
        /// Use this if you do not care about referencing (e.g. with adding two hardcoded values
        /// </summary>
        /// <param name="celObject"></param>
        public static void Push(object value)
        {
            CelStack.Push(new CelesteObject(value));
        }

        /// <summary>
        /// Pushes an already created Celeste object onto the stack
        /// </summary>
        /// <param name="celObject"></param>
        public static void Push(CelesteObject celObject)
        {
            CelStack.Push(celObject);
        }

        #endregion

        #region Pop Functions

        /// <summary>
        /// Pops an object from the top of our stack
        /// </summary>
        public static CelesteObject Pop()
        {
            Debug.Assert(CelStack.Count > 0);
            return CelStack.Pop() as CelesteObject;
        }

        #endregion

        public static void Clear()
        {
            CelStack.Clear();
        }
    }
}
