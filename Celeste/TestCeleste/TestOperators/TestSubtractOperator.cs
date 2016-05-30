using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TestCeleste
{
    [TestClass]
    public class TestSubtractOperator : CelesteUnitTest
    {
        [TestMethod]
        public void TestSubtractOperatorAddNumbers()
        {
            CelesteScript script = new CelesteScript("TestScripts\\Operators\\Subtract\\TestSubtractOperatorNumbers.cel");
            script.Run();

            CelesteTestUtils.CheckStackSize(2);
            CelesteTestUtils.CheckStackResult(10.0f);
            CelesteTestUtils.CheckStackResult(15.0f);
        }

        [TestMethod]
        public void TestSubtractOperatorAddStrings()
        {
            CelesteScript script = new CelesteScript("TestScripts\\Operators\\Subtract\\TestSubtractOperatorStrings.cel");
            script.Run();

            CelesteTestUtils.CheckStackSize(2);
            CelesteTestUtils.CheckStackResult("testsubtracting");
            CelesteTestUtils.CheckStackResult("test");
        }

        [TestMethod]
        public void TestSubtractOperatorSubtractLists()
        {
            CelesteScript script = new CelesteScript("TestScripts\\Operators\\Subtract\\TestSubtractOperatorLists.cel");
            script.Run();

            CelesteTestUtils.CheckStackSize(1);

            List<object> expected = new List<object>()
            {
                5.0f,
                "Test",
                true,
            };

            CelesteTestUtils.CheckStackResultList(expected);
            CelesteTestUtils.CheckLocalVariableList(script, "subtractedList", expected);
        }

        [TestMethod]
        public void TestSubtractOperatorSubtractTables()
        {
            CelesteScript script = new CelesteScript("TestScripts\\Operators\\Subtract\\TestSubtractOperatorTables.cel");
            script.Run();

            CelesteTestUtils.CheckStackSize(2);

            Dictionary<object, object> expected = new Dictionary<object, object>()
            {
                { "key", "value" },
            };

            CelesteObject celObject = CelesteStack.Pop();
            Assert.IsTrue(celObject.IsTable());

            Dictionary<object, object> actual = celObject.AsTable();
            Assert.AreEqual(expected["key"], actual["key"]);

            Assert.IsTrue(script.ScriptScope.VariableExists("subtractTable2"));
            actual = script.ScriptScope.GetLocalVariable("subtractTable2").GetReferencedValue<Dictionary<object, object>>();
            Assert.AreEqual(expected["key"], actual["key"]);



            expected = new Dictionary<object, object>()
            {
                { 1.0f, true },
            };

            celObject = CelesteStack.Pop();
            Assert.IsTrue(celObject.IsTable());

            actual = celObject.AsTable();
            Assert.AreEqual(expected[1.0f], actual[1.0f]);

            Assert.IsTrue(script.ScriptScope.VariableExists("subtractTable"));
            actual = script.ScriptScope.GetLocalVariable("subtractTable").GetReferencedValue<Dictionary<object, object>>();
            Assert.AreEqual(expected[1.0f], actual[1.0f]);
        }
    }
}
