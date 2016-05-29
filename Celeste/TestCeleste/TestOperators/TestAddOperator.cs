using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste
{
    [TestClass]
    public class TestAddOperator
    {
        [TestMethod]
        public void TestAddOperatorAddNumbers()
        {
            CelesteScript script = new CelesteScript("TestScripts\\Operators\\Add\\TestAddOperatorNumbers.cel");
            script.Run();

            TestOperatorUtils.CheckStackSize(2);
            TestOperatorUtils.CheckStackResult(10.0f);
            TestOperatorUtils.CheckStackResult(15.0f);
        }

        [TestMethod]
        public void TestAddOperatorAddStrings()
        {
            CelesteScript script = new CelesteScript("TestScripts\\Operators\\Add\\TestAddOperatorStrings.cel");
            script.Run();

            TestOperatorUtils.CheckStackSize(1);
            TestOperatorUtils.CheckStackResult("testadding");
        }
    }
}
