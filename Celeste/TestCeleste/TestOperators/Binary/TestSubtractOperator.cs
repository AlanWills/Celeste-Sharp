using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TestCeleste
{
    [TestClass]
    public class TestSubtractOperator : CelesteUnitTest
    {
        [TestMethod]
        public void TestSubtractOperatorSubtractNumbers()
        {
            CelesteScript script = RunScript("Operators\\Binary\\Subtract\\TestSubtractOperatorNumbers.cel");

            script.CheckLocalVariable("intSubtract", 5.0f);
            script.CheckLocalVariable("floatSubtract", 5.0f);
            script.CheckLocalVariable("multiSubtract", 10.0f);
        }

        [TestMethod]
        public void TestSubtractOperatorSubractStrings()
        {
            CelesteScript script = RunScript("Operators\\Binary\\Subtract\\TestSubtractOperatorStrings.cel");

            script.CheckLocalVariable("subtracting", "test");
            script.CheckLocalVariable("noSubtracting", "testsubtracting");
        }

        [TestMethod]
        public void TestSubtractOperatorSubtractLists()
        {
            CelesteScript script = RunScript("Operators\\Binary\\Subtract\\TestSubtractOperatorLists.cel");

            List<object> expected = new List<object>()
            {
                5.0f,
                "Test",
                true,
            };

            script.CheckLocalVariableList("subtractedList", expected);
        }

        [TestMethod]
        public void TestSubtractOperatorSubtractTables()
        {
            CelesteScript script = RunScript("Operators\\Binary\\Subtract\\TestSubtractOperatorTables.cel");

            Dictionary<object, object> expected = new Dictionary<object, object>()
            {
                { "key", "value" },
            };

            Assert.IsTrue(script.ScriptScope.VariableExists("subtractTable2"));

            Dictionary<object, object>  actual = script.ScriptScope.GetLocalVariable("subtractTable2").GetReferencedValue<Dictionary<object, object>>();
            Assert.AreEqual(expected["key"], actual["key"]);



            expected = new Dictionary<object, object>()
            {
                { 1.0f, true },
            };

            Assert.IsTrue(script.ScriptScope.VariableExists("subtractTable"));
            actual = script.ScriptScope.GetLocalVariable("subtractTable").GetReferencedValue<Dictionary<object, object>>();
            Assert.AreEqual(expected[1.0f], actual[1.0f]);
        }
    }
}
