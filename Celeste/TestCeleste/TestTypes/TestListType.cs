using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using UnitTestGameFramework;

namespace TestCeleste
{
    [TestClass]
    public class TestListType : CelesteUnitTest
    {
        [TestMethod]
        public void TestListTypeParsing()
        {
            CelesteScript script = new CelesteScript("TestScripts\\Types\\List\\TestListParsing.cel");
            script.Run();

            Assert.AreEqual(5, CelesteStack.StackSize);

            {
                CelesteObject celObject = CelesteStack.Pop();
                Assert.IsTrue(celObject.IsList());

                List<object> list = celObject.AsList();
                Assert.AreEqual(1, list.Count);

                List<object> embeddedList = (List<object>)list[0];
                Assert.AreEqual(3, embeddedList.Count);
                Assert.AreEqual(5.0f, (float)embeddedList[0]);
                Assert.AreEqual("Test", (string)embeddedList[1]);
                Assert.AreEqual(true, (bool)embeddedList[2]);
            }
            {
                CelesteObject celObject = CelesteStack.Pop();
                Assert.IsTrue(celObject.IsList());

                List<object> list = celObject.AsList();
                Assert.AreEqual(3, list.Count);
                Assert.AreEqual(true, (bool)list[0]);
                Assert.AreEqual(true, (bool)list[1]);
                Assert.AreEqual(true, (bool)list[2]);
            }
            {
                CelesteObject celObject = CelesteStack.Pop();
                Assert.IsTrue(celObject.IsList());

                List<object> list = celObject.AsList();
                Assert.AreEqual(3, list.Count);
                Assert.AreEqual("Test", list[0]);
                Assert.AreEqual("Test", list[1]);
                Assert.AreEqual("Test", list[2]);
            }
            {
                CelesteObject celObject = CelesteStack.Pop();
                Assert.IsTrue(celObject.IsList());

                List<object> list = celObject.AsList();
                Assert.AreEqual(3, list.Count);
                Assert.AreEqual(5.0f, list[0]);
                Assert.AreEqual(5.0f, list[1]);
                Assert.AreEqual(5.0f, list[2]);
            }
            {
                CelesteObject celObject = CelesteStack.Pop();
                Assert.IsTrue(celObject.IsList());

                List<object> list = celObject.AsList();
                Assert.AreEqual(3, list.Count);
                Assert.AreEqual(5.0f, (float)list[0]);
                Assert.AreEqual("Test", (string)list[1]);
                Assert.AreEqual(true, (bool)list[2]);
            }
        }

        [TestMethod]
        public void TestListTypeAssignment()
        {
            CelesteScript script = new CelesteScript("TestScripts\\Types\\List\\TestListAssignment.cel");
            script.Run();

            CelesteTestUtils.CheckStackSize(0);

            {
                Assert.IsTrue(script.ScriptScope.VariableExists("firstList"));
                Variable variable = script.ScriptScope.GetLocalVariable("firstList");
                List<object> expected = new List<object>()
                {
                    5.0f,
                    "Test",
                    true
                };

                TestListHelperFunctions.CheckOrderedListsEqual(expected, variable.GetReferencedValue<List<object>>());
            }
            {
                Assert.IsTrue(script.ScriptScope.VariableExists("secondList"));
                Variable variable = script.ScriptScope.GetLocalVariable("secondList");
                List<object> expected = new List<object>()
                {
                    5.0f,
                    5.0f,
                    5.0f
                };

                TestListHelperFunctions.CheckOrderedListsEqual(expected, variable.GetReferencedValue<List<object>>());
            }
            {
                Assert.IsTrue(script.ScriptScope.VariableExists("thirdList"));
                Variable variable = script.ScriptScope.GetLocalVariable("thirdList");
                List<object> expected = new List<object>()
                {
                    "Test",
                    "Test",
                    "Test"
                };

                TestListHelperFunctions.CheckOrderedListsEqual(expected, variable.GetReferencedValue<List<object>>());
            }
            {
                Assert.IsTrue(script.ScriptScope.VariableExists("fourthList"));
                Variable variable = script.ScriptScope.GetLocalVariable("fourthList");
                List<object> expected = new List<object>()
                {
                    true,
                    true,
                    true
                };

                TestListHelperFunctions.CheckOrderedListsEqual(expected, variable.GetReferencedValue<List<object>>());
            }
            {
                Assert.IsTrue(script.ScriptScope.VariableExists("fifthList"));
                Variable variable = script.ScriptScope.GetLocalVariable("fifthList");
                List<object> expected = new List<object>()
                {
                    5.0f,
                    "Test",
                    true
                };

                List<object> actualList = variable.GetReferencedValue<List<object>>()[0] as List<object>;
                TestListHelperFunctions.CheckOrderedListsEqual(expected, actualList);
            }
        }
    }
}