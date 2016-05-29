using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste
{
    [TestClass]
    public class TestDivideOperator
    {
        [TestMethod]
        public void TestDivideOperatorAddNumbers()
        {
            CelesteScript script = new CelesteScript("TestScripts\\Operators\\Divide\\TestDivideOperatorNumbers.cel");
            script.Run();

            TestOperatorUtils.CheckStackSize(3);
            TestOperatorUtils.CheckStackResult(1.0f);
            TestOperatorUtils.CheckStackResult(0.2f);
            TestOperatorUtils.CheckStackResult(5.0f);
        }
    }
}
