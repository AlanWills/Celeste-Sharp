using System;
using System.Collections.Generic;
using System.Diagnostics;
using sysDel = System.Delegate;
using BindingPair = System.Tuple<System.Type, System.Type>;

namespace Celeste
{
    public static class CelesteBinder
    {
        private static Dictionary<BindingPair, sysDel> Bindings = new Dictionary<BindingPair, sysDel>(new BindingsComparer());

        public static void Init()
        {
            AddBinding(typeof(float), typeof(string), (Func<object, string>)ToString);
            AddBinding(typeof(bool), typeof(string), (Func<object, string>)ToString);
            AddBinding(typeof(Reference), typeof(string), (Func<object, string>)ToString);
        }

        internal static void ClearBindings()
        {
            Bindings.Clear();
        }

        public static bool AddBinding(Type bindFrom, Type bindTo, sysDel bindFunction)
        {
            BindingPair binding = new BindingPair(bindFrom, bindTo);
            if (Bindings.ContainsKey(binding))
            {
                Debug.Fail("Could not add binding for " + bindFrom.Name +  " and " + bindTo.Name + " as a binding already exists");
                return false;
            }

            Bindings.Add(binding, bindFunction);
            return true;
        }

        public static object Bind(object input, Type bindTo)
        {
            BindingPair binding = new BindingPair(input.GetType(), bindTo);
            Debug.Assert(Bindings.ContainsKey(binding), "No binding exists for " + input.GetType().Name + " to " + bindTo.Name);

            return Bindings[binding].DynamicInvoke(input);
        }

        #region Bindings

        private static string ToString(object input)
        {
            return input.ToString();
        }

        #endregion
    }
}
