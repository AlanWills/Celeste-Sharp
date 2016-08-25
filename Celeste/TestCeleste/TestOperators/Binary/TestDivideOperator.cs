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
            CelesteScript script = RunScript("Operators\\Divide\\TestDivideOperatorNumbers.cel");

            script.CheckLocalVariable("intDivide", 5.0f);
            script.CheckLocalVariable("floatDivide", 0.2f);
            script.CheckLocalVariable("multiDivide", 1.0f);
        }
    }
}
