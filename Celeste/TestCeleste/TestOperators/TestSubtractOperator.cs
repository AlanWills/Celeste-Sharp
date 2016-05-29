using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste
{
    [TestClass]
    public class TestSubtractOperator : CelesteUnitTest
    {
        [TestMethod]
        public void TestSubtractOperatorAddNumbers()
        {
            CelesteScript script = new CelesteScript("TestScripts\\Operators\\Subtract\\TestSubtractOperatorNumbers.cel");
            script.Run();

            CelesteTestUtils.CheckStackSize(2);
            CelesteTestUtils.CheckStackResult(10.0f);
            CelesteTestUtils.CheckStackResult(15.0f);
        }

        [TestMethod]
        public void TestSubtractOperatorAddStrings()
        {
            CelesteScript script = new CelesteScript("TestScripts\\Operators\\Subtract\\TestSubtractOperatorStrings.cel");
            script.Run();

            CelesteTestUtils.CheckStackSize(2);
            CelesteTestUtils.CheckStackResult("testsubtracting");
            CelesteTestUtils.CheckStackResult("test");
        }
    }
}
