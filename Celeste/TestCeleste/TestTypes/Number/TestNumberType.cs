using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste.TestTypes.Number
{
    [TestClass]
    public class TestNumberType
    {
        [TestMethod]
        public void TestNumberTypeParsing()
        {
            CelesteScript script = new CelesteScript("TestScripts\\Types\\Number\\TestNumberParsing.cel");
            script.Run();

            Assert.AreEqual(4, CelesteStack.StackSize);

            CelesteObject celObject = CelesteStack.Pop();
            Assert.IsTrue(celObject.IsNumber());
            Assert.AreEqual(-5, celObject.As<float>());

            CelesteObject celObject2 = CelesteStack.Pop();
            Assert.IsTrue(celObject2.IsNumber());
            Assert.AreEqual(0, celObject2.As<float>());

            CelesteObject celObject3 = CelesteStack.Pop();
            Assert.IsTrue(celObject3.IsNumber());
            Assert.AreEqual(5, celObject3.As<float>());

            CelesteObject celObject4 = CelesteStack.Pop();
            Assert.IsTrue(celObject4.IsNumber());
            Assert.AreEqual(10, celObject4.As<float>());
        }
    }
}
