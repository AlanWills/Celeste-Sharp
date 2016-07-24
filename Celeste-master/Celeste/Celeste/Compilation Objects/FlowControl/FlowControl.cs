using System.Collections.Generic;
using System.Diagnostics;

namespace Celeste
{
    /// <summary>
    /// A class responsible for altering the logical flow of code via condition functions.
    /// </summary>
    internal abstract class FlowControl : CompiledStatement
    {
        #region Properties and Fields

        /// <summary>
        /// An ordered list of conditions with corresponding bodies that will be executed in turn.
        /// If a condition (Key) evaluates as true then the body (Value) will be executed.
        /// </summary>
        protected List<KeyValuePair<CompiledStatement, CompiledStatement>> ConditionsAndBodies { get; private set; } 

        /// <summary>
        /// The scope for the condtion and body of this flow control (so condition variables do not leak out).
        /// </summary>
        protected Scope FlowScope { get; private set; }

        #endregion

        public FlowControl()
        {
            FlowScope = new Scope();
            ConditionsAndBodies = new List<KeyValuePair<CompiledStatement, CompiledStatement>>();
        }

        #region Virtual Functions

        /// <summary>
        /// Iterates through the list of conditions and performs each one in turn.  Checks the top element of the stack to see
        /// if a true value has been pushed on.  If it has it performs the corresponding compiled statement in that KVP.
        /// </summary>
        public override void PerformOperation()
        {
            base.PerformOperation();

            CelesteStack.CurrentScope = FlowScope;

            Debug.Assert(ConditionsAndBodies.Count > 0, "No conditions or functions specified for flow control");
            foreach (KeyValuePair<CompiledStatement, CompiledStatement> condBody in ConditionsAndBodies)
            {
                condBody.Key.PerformOperation();

                Debug.Assert(CelesteStack.StackSize > 0, "No true or false value pushed onto stack");
                CelesteObject celObject = CelesteStack.Pop();

                if (celObject.IsBool())
                {
                    if (celObject.As<bool>())
                    {
                        condBody.Value.PerformOperation();
                        break;
                    }
                }
                else
                {
                    Debug.Fail("Condition must be evaluatable to a bool value");
                }
            }

            CelesteStack.Scopes.Remove(FlowScope);
            CelesteStack.CurrentScope = FlowScope.ParentScope;
        }

        #endregion
    }
}