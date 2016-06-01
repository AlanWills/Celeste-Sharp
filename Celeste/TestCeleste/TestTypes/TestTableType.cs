using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TestCeleste.TestTypes
{
    [TestClass]
    public class TestTableType : CelesteUnitTest
    {
        [TestMethod]
        public void TestTableTypeAssignment()
        {
            CelesteScript script = RunScript("TestScripts\\Types\\Table\\TestTableParsing.cel");

            {
                Assert.IsTrue(script.ScriptScope.VariableExists("firstTable"));
                Variable variable = script.ScriptScope.GetLocalVariable("firstTable");
                Dictionary<object, object> expected = new Dictionary<object, object>()
                {
                    { "Test", 5.0f },
                    { 10.0f, true }
                };

                Dictionary<object, object> actual = variable.GetReferencedValue<Dictionary<object, object>>();
                Assert.AreEqual(expected["Test"], actual["Test"]);
                Assert.AreEqual(expected[10.0f], actual[10.0f]);
            }
            {
                Assert.IsTrue(script.ScriptScope.VariableExists("secondTable"));
                Variable variable = script.ScriptScope.GetLocalVariable("secondTable");
                Dictionary<object, object> expected = new Dictionary<object, object>()
                {
                    { "Test", 5.0f },
                    { 10.0f, true }
                };

                Dictionary<object, object> actual = variable.GetReferencedValue<Dictionary<object, object>>();
                Assert.AreEqual(expected["Test"], actual["Test"]);
                Assert.AreEqual(expected[10.0f], actual[10.0f]);
            }
            {
                Assert.IsTrue(script.ScriptScope.VariableExists("thirdTable"));
                Variable variable = script.ScriptScope.GetLocalVariable("thirdTable");
                Dictionary<object, object> expected = new Dictionary<object, object>()
                {
                    { "Test", 5.0f },
                    { 10.0f, true }
                };

                Dictionary<object, object> actual = variable.GetReferencedValue<Dictionary<object, object>>();
                Assert.AreEqual(expected["Test"], actual["Test"]);
                Assert.AreEqual(expected[10.0f], actual[10.0f]);

                expected = new Dictionary<object, object>()
                {
                    { "Embedded Test", 10.0f },
                    { 20.0f, false }
                };

                actual = actual["Table"] as Dictionary<object, object>;
                Assert.AreEqual(expected["Embedded Test"], actual["Embedded Test"]);
                Assert.AreEqual(expected[20.0f], actual[20.0f]);
            }
        }
    }
}
