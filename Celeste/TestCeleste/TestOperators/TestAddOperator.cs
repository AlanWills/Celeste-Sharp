using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TestCeleste
{
    [TestClass]
    public class TestAddOperator : CelesteUnitTest
    {
        [TestMethod]
        public void TestAddOperatorAddNumbers()
        {
            CelesteScript script = RunScript("Operators\\Add\\TestAddOperatorNumbers.cel");
            
            script.CheckLocalVariable("addNumbers", 15.0f);
            script.CheckLocalVariable("addMultipleNumbers", 10.0f);
        }

        [TestMethod]
        public void TestAddOperatorAddStrings()
        {
            CelesteScript script = RunScript("Operators\\Add\\TestAddOperatorStrings.cel");

            script.CheckLocalVariable("stringAdding", "testadding");
        }

        [TestMethod]
        public void TestAddOperatorAddLists()
        {
            CelesteScript script = RunScript("Operators\\Add\\TestAddOperatorLists.cel");

            List<object> expected = new List<object>()
            {
                5.0f,
                "Test",
                true,
                10.0f,
                "List Adding",
                false
            };

            script.CheckLocalVariableList("addedList", expected);
        }

        [TestMethod]
        public void TestAddOperatorAddTables()
        {
            CelesteScript script = RunScript("Operators\\Add\\TestAddOperatorTables.cel");

            Dictionary<object, object> expected = new Dictionary<object, object>()
            {
                { "key", "value" },
                { "secondKey", "secondValue" },
            };

            Assert.IsTrue(script.ScriptScope.VariableExists("addTable2"));
            Dictionary<object, object> actual = script.ScriptScope.GetLocalVariable("addTable2").GetReferencedValue<Dictionary<object, object>>();
            Assert.AreEqual(expected["key"], actual["key"]);
            Assert.AreEqual(expected["secondKey"], actual["secondKey"]);



            expected = new Dictionary<object, object>()
            {
                { 1.0f, true },
                { 2.0f, false },
            };

            Assert.IsTrue(script.ScriptScope.VariableExists("addTable"));
            actual = script.ScriptScope.GetLocalVariable("addTable").GetReferencedValue<Dictionary<object, object>>();
            Assert.AreEqual(expected[1.0f], actual[1.0f]);
            Assert.AreEqual(expected[2.0f], actual[2.0f]);

        }
    }
}