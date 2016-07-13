using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace TestCeleste
{
    [TestClass]
    public class TestLogErrorCmd : CelesteUnitTest
    {
        [TestMethod]
        public void TestLogErrorCmdHardCodedValues()
        {
            Cel.ClearLog();
            CelesteScript script = RunScript("ScriptCommands\\Core\\LogErrorCmd\\TestLogErrorCmdHardCodedValues.cel");

            Assert.IsTrue(CelesteStack.GlobalScope.VariableExists("logError"));

            // This should definitely exist!
            using (StreamReader reader = Cel.LogReader)
            {
                Assert.AreEqual("hello", reader.ReadLine());
                Assert.AreEqual("True", reader.ReadLine());
                Assert.AreEqual("1", reader.ReadLine());
                Assert.AreEqual("-1", reader.ReadLine());
            }
        }

        [TestMethod]
        public void TestLogErrorCmdVariables()
        {
            Cel.ClearLog();
            CelesteScript script = RunScript("ScriptCommands\\Core\\LogErrorCmd\\TestLogErrorCmdVariables.cel");

            Assert.IsTrue(CelesteStack.GlobalScope.VariableExists("logError"));

            // This should definitely exist!
            using (StreamReader reader = Cel.LogReader)
            {
                Assert.AreEqual("hello", reader.ReadLine());
                Assert.AreEqual("True", reader.ReadLine());
                Assert.AreEqual("10", reader.ReadLine());
                Assert.AreEqual("-10", reader.ReadLine());
                Assert.AreEqual("reference", reader.ReadLine());
            }
        }
    }
}