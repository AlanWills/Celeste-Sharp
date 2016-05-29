using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste
{
    [TestClass]
    public class TestMultiplyOperator
    {
        [TestMethod]
        public void TestMultiplyOperatorAddNumbers()
        {
            CelesteScript script = new CelesteScript("TestScripts\\Operators\\Multiply\\TestMultiplyOperatorNumbers.cel");
            script.Run();

            TestOperatorUtils.CheckStackSize(3);
            TestOperatorUtils.CheckStackResult(0);
            TestOperatorUtils.CheckStackResult(2.0);
            TestOperatorUtils.CheckStackResult(20.0);
        }
    }
}
