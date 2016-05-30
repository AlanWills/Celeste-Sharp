using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TestCeleste
{
    [TestClass]
    public class TestAddOperator : CelesteUnitTest
    {
        [TestMethod]
        public void TestAddOperatorAddNumbers()
        {
            CelesteScript script = new CelesteScript("TestScripts\\Operators\\Add\\TestAddOperatorNumbers.cel");
            script.Run();

            CelesteTestUtils.CheckStackSize(2);
            CelesteTestUtils.CheckStackResult(10.0f);
            CelesteTestUtils.CheckStackResult(15.0f);
        }

        [TestMethod]
        public void TestAddOperatorAddStrings()
        {
            CelesteScript script = new CelesteScript("TestScripts\\Operators\\Add\\TestAddOperatorStrings.cel");
            script.Run();

            CelesteTestUtils.CheckStackSize(1);
            CelesteTestUtils.CheckStackResult("testadding");
        }

        [TestMethod]
        public void TestAddOperatorAddLists()
        {
            CelesteScript script = new CelesteScript("TestScripts\\Operators\\Add\\TestAddOperatorLists.cel");
            script.Run();

            CelesteTestUtils.CheckStackSize(1);

            List<object> expected = new List<object>()
            {
                5.0f,
                "Test",
                true,
                10.0f,
                "List Adding",
                false
            };

            CelesteTestUtils.CheckStackResultList(expected);
            CelesteTestUtils.CheckLocalVariableList(script, "addedList", expected);
        }

        [TestMethod]
        public void TestAddOperatorAddTables()
        {
            CelesteScript script = new CelesteScript("TestScripts\\Operators\\Add\\TestAddOperatorTables.cel");
            script.Run();

            CelesteTestUtils.CheckStackSize(2);

            Dictionary<object, object> expected = new Dictionary<object, object>()
            {
                { "key", "value" },
                { "secondKey", "secondValue" },
            };

            CelesteObject celObject = CelesteStack.Pop();
            Assert.IsTrue(celObject.IsTable());

            Dictionary<object, object> actual = celObject.AsTable();
            Assert.AreEqual(expected["key"], actual["key"]);
            Assert.AreEqual(expected["secondKey"], actual["secondKey"]);

            Assert.IsTrue(script.ScriptScope.VariableExists("addTable2"));
            actual = script.ScriptScope.GetLocalVariable("addTable2").GetReferencedValue<Dictionary<object, object>>();
            Assert.AreEqual(expected["key"], actual["key"]);
            Assert.AreEqual(expected["secondKey"], actual["secondKey"]);



            expected = new Dictionary<object, object>()
            {
                { 1.0f, true },
                { 2.0f, false },
            };

            celObject = CelesteStack.Pop();
            Assert.IsTrue(celObject.IsTable());

            actual = celObject.AsTable();
            Assert.AreEqual(expected[1.0f], actual[1.0f]);
            Assert.AreEqual(expected[2.0f], actual[2.0f]);

            Assert.IsTrue(script.ScriptScope.VariableExists("addTable"));
            actual = script.ScriptScope.GetLocalVariable("addTable").GetReferencedValue<Dictionary<object, object>>();
            Assert.AreEqual(expected[1.0f], actual[1.0f]);
            Assert.AreEqual(expected[2.0f], actual[2.0f]);

        }
    }
}