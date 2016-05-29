using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste
{
    [TestClass]
    public class TestDivideOperator : CelesteUnitTest
    {
        [TestMethod]
        public void TestDivideOperatorAddNumbers()
        {
            CelesteScript script = new CelesteScript("TestScripts\\Operators\\Divide\\TestDivideOperatorNumbers.cel");
            script.Run();

            CelesteTestUtils.CheckStackSize(3);
            CelesteTestUtils.CheckStackResult(1.0f);
            CelesteTestUtils.CheckStackResult(0.2f);
            CelesteTestUtils.CheckStackResult(5.0f);
        }
    }
}
