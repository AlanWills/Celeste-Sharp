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
            CelesteScript script = RunScript("Types\\List\\TestListParsing.cel");

            {
                List<object> expected = new List<object>() { };

                script.CheckLocalVariableList("emptyList", expected);
            }
            {
                List<object> expected = new List<object>()
                {
                    5.0f,
                    "Test",
                    true,
                };

                script.CheckLocalVariableList("oneLineList", expected);
            }
            {
                List<object> expected = new List<object>()
                {
                    5.0f,
                    5.0f,
                    5.0f,
                };

                script.CheckLocalVariableList("multiLineList", expected);
            }
            {
                List<object> expected = new List<object>()
                {
                    "Test",
                    "Test",
                    "Test",
                };

                script.CheckLocalVariableList("strangeFormatList", expected);
            }
            {
                List<object> expected = new List<object>()
                {
                    true,
                    true,
                    true,
                };

                script.CheckLocalVariableList("strangeFormatList2", expected);
            }
            {
                Assert.IsTrue(script.ScriptScope.VariableExists("indentedList"));
                List<object> indentedList = script.ScriptScope.GetLocalVariable("indentedList").GetReferencedValue<List<object>>();
                Assert.AreEqual(1, indentedList.Count);

                List<object> expected = new List<object>()
                {
                    5.0f,
                    "Test",
                    true
                };

                List<object> embeddedList = (List<object>)indentedList[0];
                Assert.IsTrue(TestHelperFunctions.CheckOrderedListsEqual(expected, embeddedList));
            }
        }
    }
}