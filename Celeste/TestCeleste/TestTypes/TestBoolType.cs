using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste
{
    [TestClass]
    public class TestBoolType : CelesteUnitTest
    {
        [TestMethod]
        public void TestBoolTypeParsing()
        {
            CelesteScript script = new CelesteScript("TestScripts\\Types\\Bool\\TestBoolParsing.cel");
            script.Run();

            Assert.AreEqual(2, CelesteStack.StackSize);

            CelesteObject celObject = CelesteStack.Pop();
            Assert.IsTrue(celObject.IsBool());
            Assert.AreEqual(false, celObject.As<bool>());

            CelesteObject celObject2 = CelesteStack.Pop();
            Assert.IsTrue(celObject2.IsBool());
            Assert.AreEqual(true, celObject2.As<bool>());
        }
    }
}
