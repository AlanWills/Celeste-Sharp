using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste.TestOperators
{
    [TestClass]
    public class TestEqualityOperator : CelesteUnitTest
    {
        [TestMethod]
        public void TestEqualityOperatorInitialAssignment()
        {
            CelesteScript script = new CelesteScript("TestScripts\\Operators\\Equality\\TestEqualityOperator.cel");
            script.Run();
            
            CelesteTestUtils.CheckStackSize(0);
            CelesteTestUtils.CheckLocalVariable(script, "number", 5.0f);
            CelesteTestUtils.CheckLocalVariable(script, "string", "Test");
        }

        [TestMethod]
        public void TestEqualityOperatorReassignment()
        {
            CelesteScript script = new CelesteScript("TestScripts\\Operators\\Equality\\TestEqualityOperatorReassignment.cel");
            script.Run();

            CelesteTestUtils.CheckStackSize(0);
            CelesteTestUtils.CheckLocalVariable(script, "number", "Test");
        }
    }
}