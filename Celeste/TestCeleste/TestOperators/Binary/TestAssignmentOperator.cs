using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste.TestOperators
{
    [TestClass]
    public class TestAssignmentOperator : CelesteUnitTest
    {
        [TestMethod]
        public void TestAssignmentOperatorInitialAssignment()
        {
            CelesteScript script = RunScript("Operators\\Binary\\Assignment\\TestAssignmentOperator.cel");
            
            script.CheckLocalVariable("number", 5.0f);
            script.CheckLocalVariable("string", "Test");
        }

        [TestMethod]
        public void TestAssignmentOperatorReassignment()
        {
            CelesteScript script = RunScript("Operators\\Binary\\Assignment\\TestAssignmentOperatorReassignment.cel");

            script.CheckLocalVariable("number", "Test");
        }
    }
}