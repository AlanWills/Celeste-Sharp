using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste.TestTypes.Number
{
    [TestClass]
    public class TestStringType
    {
        [TestMethod]
        public void TestStringTypeParsing()
        {
            CelesteScript script = new CelesteScript("TestScripts\\Types\\String\\TestStringParsing.cel");
            script.Run();

            Assert.AreEqual(5, CelesteStack.StackSize);

            CelesteObject celObject = CelesteStack.Pop();
            Assert.IsTrue(celObject.IsString());
            Assert.AreEqual("test really long sentence with lots of spaces", celObject.As<string>());

            CelesteObject celObject2 = CelesteStack.Pop();
            Assert.IsTrue(celObject2.IsString());
            Assert.AreEqual("test two spaces", celObject2.As<string>());

            CelesteObject celObject3 = CelesteStack.Pop();
            Assert.IsTrue(celObject3.IsString());
            Assert.AreEqual("test space", celObject3.As<string>());

            CelesteObject celObject4 = CelesteStack.Pop();
            Assert.IsTrue(celObject4.IsString());
            Assert.AreEqual("test", celObject4.As<string>());

            CelesteObject celObject5 = CelesteStack.Pop();
            Assert.IsTrue(celObject5.IsString());
            Assert.AreEqual("test", celObject5.As<string>());
        }
    }
}
