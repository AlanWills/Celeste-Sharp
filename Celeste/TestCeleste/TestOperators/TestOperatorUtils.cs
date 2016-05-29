﻿using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste
{
    public static class TestOperatorUtils
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

        public static void CheckLocalVariable(CelesteScript script, string variableName, object expected)
        {
            Assert.IsTrue(script.ScriptScope.VariableExists(variableName));

            Reference varRef = script.ScriptScope.GetLocalVariable(variableName)._Value as Reference;
            Assert.IsNotNull(varRef);
            Assert.AreEqual(expected, varRef.Value);
        }
    }
}
