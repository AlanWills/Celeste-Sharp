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
    }
}
