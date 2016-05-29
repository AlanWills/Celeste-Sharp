using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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

        // Do lists of numbers, string and bools only
        // Do list within list
    }
}
