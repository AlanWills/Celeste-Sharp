using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste.TestFlowControls
{
    [TestClass]
    public class TestWhileFlowControl : CelesteUnitTest
    {
        [TestMethod]
        public void TestWhileFlowControlParsing()
        {
            CelesteScript script = RunScript("FlowControl\\TestWhileFlowControlParsing.cel");
        }

        [TestMethod]
        public void TestWhileFlowControlSimple()
        {
            CelesteScript script = RunScript("FlowControl\\TestWhileFlowControlSimple.cel");

            Assert.AreEqual(1, script.ScriptScope.VariableCount);
            script.CheckLocalVariable("variable", 10.0f);
        }

        [TestMethod]
        public void TestWhileFlowControlComplex()
        {
        }

        [TestMethod]
        public void TestWhileFlowControlUseVariableAsCondition()
        {
        }
    }
}
