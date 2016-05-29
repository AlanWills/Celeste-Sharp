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
            CelesteScript script = new CelesteScript("TestScripts\\Operators\\Multiply\\TestMultiplyOperatorNumbers.cel");
            script.Run();

            CelesteTestUtils.CheckStackSize(3);
            CelesteTestUtils.CheckStackResult(0);
            CelesteTestUtils.CheckStackResult(2.0);
            CelesteTestUtils.CheckStackResult(20.0);
        }
    }
}
