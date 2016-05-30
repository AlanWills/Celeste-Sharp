using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using UnitTestGameFramework;

namespace TestCeleste
{
    public static class CelesteTestUtils
    {
        public static void CheckStackSize(int expected)
        {
            Assert.AreEqual(expected, CelesteStack.StackSize);
        }

        public static void CheckStackResult<T>(T expected)
        {
            CelesteObject actual = CelesteStack.Pop();
            Assert.AreEqual(expected, actual.As<T>());
        }

        public static void CheckStackResultList(List<object> expected)
        {
            CelesteObject actual = CelesteStack.Pop();
            Assert.IsTrue(actual.IsList());
            TestListHelperFunctions.CheckOrderedListsEqual(expected, actual.AsList());
        }

        public static void CheckLocalVariable(CelesteScript script, string variableName, object expected)
        {
            Assert.IsTrue(script.ScriptScope.VariableExists(variableName));
            Assert.AreEqual(expected, script.ScriptScope.GetLocalVariable(variableName).GetReferencedValue<object>());
        }

        public static void CheckLocalVariableList(CelesteScript script, string variableName, List<object> expected)
        {
            Assert.IsTrue(script.ScriptScope.VariableExists(variableName));
            TestListHelperFunctions.CheckOrderedListsEqual(expected, script.ScriptScope.GetLocalVariable(variableName).GetReferencedValue<List<object>>());
        }

        public static void CheckGlobalVariable(string variableName, object expected)
        {
            Assert.IsTrue(CelesteStack.GlobalScope.VariableExists(variableName));
            Assert.AreEqual(expected, CelesteStack.GlobalScope.GetLocalVariable(variableName).GetReferencedValue<object>());
        }
    }
}
