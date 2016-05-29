using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TestCeleste.TestTypes
{
    [TestClass]
    public class TestTableType : CelesteUnitTest
    {
        [TestMethod]
        public void TestTableTypeParsing()
        {
            CelesteScript script = new CelesteScript("TestScripts\\Types\\Table\\TestTableParsing.cel");
            script.Run();

            Assert.AreEqual(3, CelesteStack.StackSize);

            {
                CelesteObject celObject = CelesteStack.Pop();
                Assert.IsTrue(celObject.IsTable());

                Dictionary<object, object> table = celObject.AsTable();
                Assert.AreEqual(3, table.Count);

                Assert.IsTrue(table.ContainsKey("Test"));
                Assert.AreEqual(5.0f, table["Test"]);
                Assert.IsTrue(table.ContainsKey(10.0f));
                Assert.AreEqual(true, table[10.0f]);
                Assert.IsTrue(table.ContainsKey("Table"));

                {
                    Dictionary<object, object> embeddedTable = table["Table"] as Dictionary<object, object>;
                    Assert.AreEqual(2, embeddedTable.Count);

                    Assert.IsTrue(embeddedTable.ContainsKey("Embedded Test"));
                    Assert.AreEqual(10.0f, embeddedTable["Embedded Test"]);
                    Assert.IsTrue(embeddedTable.ContainsKey(20.0f));
                    Assert.AreEqual(false, embeddedTable[20.0f]);
                }
            }
            {
                CelesteObject celObject = CelesteStack.Pop();
                Assert.IsTrue(celObject.IsTable());

                Dictionary<object, object> table = celObject.AsTable();
                Assert.AreEqual(2, table.Count);

                Assert.IsTrue(table.ContainsKey("Test"));
                Assert.AreEqual(5.0f, table["Test"]);
                Assert.IsTrue(table.ContainsKey(10.0f));
                Assert.AreEqual(true, table[10.0f]);
            }
            {
                CelesteObject celObject = CelesteStack.Pop();
                Assert.IsTrue(celObject.IsTable());

                Dictionary<object, object> table = celObject.AsTable();
                Assert.AreEqual(2, table.Count);

                Assert.IsTrue(table.ContainsKey("Test"));
                Assert.AreEqual(5.0f, table["Test"]);
                Assert.IsTrue(table.ContainsKey(10.0f));
                Assert.AreEqual(true, table[10.0f]);
            }
        }

        [TestMethod]
        public void TestTableTypeAssignment()
        {
            CelesteScript script = new CelesteScript("TestScripts\\Types\\Table\\TestTableAssignment.cel");
            script.Run();

            Assert.AreEqual(0, CelesteStack.StackSize);

            {
                Assert.IsTrue(script.ScriptScope.VariableExists("firstTable"));
                Variable variable = script.ScriptScope.GetLocalVariable("firstTable");
                Dictionary<object, object> expected = new Dictionary<object, object>()
                {
                    { "Test", 5.0f },
                    { 10.0f, true }
                };

                Dictionary<object, object> actual = (variable._Value as Reference).Value as Dictionary<object, object>;
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

                Dictionary<object, object> actual = (variable._Value as Reference).Value as Dictionary<object, object>;
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

                Dictionary<object, object> actual = (variable._Value as Reference).Value as Dictionary<object, object>;
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
