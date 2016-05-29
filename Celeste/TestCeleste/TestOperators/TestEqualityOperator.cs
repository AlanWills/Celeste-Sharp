using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste.TestOperators
{
    [TestClass]
    public class TestEqualityOperator
    {
        [TestMethod]
        public void TestEqualityOperatorInitialAssignment()
        {
            CelesteScript script = new CelesteScript("TestScripts\\Operators\\Equality\\TestEqualityOperator.cel");
            script.Run();
            
            TestOperatorUtils.CheckStackSize(0);
        }

        [TestMethod]
        public void TestEqualityOperatorReassignment()
        {
            CelesteScript script = new CelesteScript("TestScripts\\Operators\\Equality\\TestEqualityOperatorReassignment.cel");
            script.Run();

            TestOperatorUtils.CheckStackSize(0);
        }
    }
}