using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using UnitTestFramework;

namespace TestCeleste
{
    [TestClass]
    public class TestReferencing : CelesteUnitTest
    {
        [TestMethod]
        public void TestReferencingCelesteObjectAffectsNumberVariable()
        {
            Reference obj = new Reference(5.0f);
            CelesteObject celesteObject = new CelesteObject(obj);

            Assert.AreEqual(5.0f, celesteObject.Value);

            obj.Value = 10.0f;
            Assert.AreEqual(10.0f, celesteObject.Value);
        }

        [TestMethod]
        public void TestReferencingCelesteObjectAffectsStringVariable()
        {
            Reference obj = new Reference("Test");
            CelesteObject celesteObject = new CelesteObject(obj);

            Assert.AreEqual("Test", celesteObject.Value);

            obj.Value = "Test Change";
            Assert.AreEqual("Test Change", celesteObject.Value);
        }

        [TestMethod]
        public void TestReferencingScopedVariables()
        {
            CelesteScript script = RunScript("TestReferencingScopedVariables.cel");

            float expected = 10.0f;
            script.CheckLocalVariable("firstVariable", expected);
            script.CheckLocalVariable("secondVariable", expected);

            expected = 5.0f;
            script.ScriptScope.GetLocalVariable("firstVariable").SetReferencedValue(expected);
            script.CheckLocalVariable("firstVariable", expected);
            script.CheckLocalVariable("secondVariable", expected);

            List<object> expectedList = new List<object>() { 10.0f };
            script.CheckLocalVariableList("firstList", expectedList);
            script.CheckLocalVariableList("secondList", expectedList);

            expectedList.Add(20.0f);
            script.ScriptScope.GetLocalVariable("firstList").SetReferencedValue(expectedList);
            script.CheckLocalVariable("firstList", expectedList);
            script.CheckLocalVariable("secondList", expectedList);

            Dictionary<object, object> expectedTable = new Dictionary<object, object> { { "key", false } };
            Assert.IsTrue(script.ScriptScope.VariableExists("firstTable"));
            Assert.IsTrue(script.ScriptScope.VariableExists("secondTable"));

            Variable first = script.ScriptScope.GetLocalVariable("firstTable");
            Dictionary<object, object> firstTable = first.GetReferencedValue<Dictionary<object, object>>();
            Dictionary<object, object> secondTable = script.ScriptScope.GetLocalVariable("secondTable").GetReferencedValue<Dictionary<object, object>>();
            Assert.AreEqual(firstTable, secondTable);
            Assert.AreEqual(false, firstTable["key"]);

            firstTable.Add(10.0f, "value");
            Assert.AreEqual(firstTable, secondTable);
            Assert.AreEqual(false, firstTable["key"]);
            Assert.AreEqual("value", firstTable[10.0f]);
        }
    }
}