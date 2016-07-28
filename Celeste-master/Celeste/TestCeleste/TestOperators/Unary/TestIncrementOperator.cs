using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste
{
    [TestClass]
    public class TestIncrementOperator : CelesteUnitTest
    {
        [TestMethod]
        public void TestIncrementOperatorPostIncrementVariable()
        {
            CelesteScript script = RunScript("Operators\\Increment\\TestIncrementOperatorPostIncrementVariable.cel");

            script.CheckLocalVariable("variable", 5.0f);
        }
    }
}