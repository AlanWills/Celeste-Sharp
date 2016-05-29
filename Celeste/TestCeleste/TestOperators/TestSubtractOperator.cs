using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste
{
    [TestClass]
    public class TestSubtractOperator
    {
        [TestMethod]
        public void TestSubtractOperatorAddNumbers()
        {
            CelesteScript script = new CelesteScript("TestScripts\\Operators\\Subtract\\TestSubtractOperatorNumbers.cel");
            script.Run();

            TestOperatorUtils.CheckStackSize(2);
            TestOperatorUtils.CheckStackResult(10.0f);
            TestOperatorUtils.CheckStackResult(15.0f);
        }

        [TestMethod]
        public void TestSubtractOperatorAddStrings()
        {
            CelesteScript script = new CelesteScript("TestScripts\\Operators\\Subtract\\TestSubtractOperatorStrings.cel");
            script.Run();

            TestOperatorUtils.CheckStackSize(2);
            TestOperatorUtils.CheckStackResult("testsubtracting");
            TestOperatorUtils.CheckStackResult("test");
        }
    }
}
