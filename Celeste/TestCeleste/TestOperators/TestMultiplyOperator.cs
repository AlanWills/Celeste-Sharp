using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste
{
    [TestClass]
    public class TestMultiplyOperator : CelesteUnitTest
    {
        [TestMethod]
        public void TestMultiplyOperatorAddNumbers()
        {
            CelesteScript script = RunScript("TestScripts\\Operators\\Multiply\\TestMultiplyOperatorNumbers.cel");

            script.CheckLocalVariable("intMultiply", 20.0f);
            script.CheckLocalVariable("multiMultiply", 2.0f);
            script.CheckLocalVariable("zeroMultiply", 0.0f);
        }
    }
}
