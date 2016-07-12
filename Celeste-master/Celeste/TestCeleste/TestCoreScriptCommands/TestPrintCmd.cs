using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace TestCeleste
{
    [TestClass]
    public class TestPrintCmd : CelesteUnitTest
    {
        [TestMethod]
        public void TestPrintCmdHardCodedValues()
        {
            // Overwrite the file - we do not want any previous test results interacting with this
            string outputFilePath = CelesteScriptManager.ScriptDirectoryPath + "\\CoreScriptCommands\\PrintCmd\\TestPrintCmdHardCodedValues.txt";
            using (StreamWriter writer = new StreamWriter(outputFilePath, false))
            {
                Console.SetOut(writer);
                CelesteScript script = RunScript("CoreScriptCommands\\PrintCmd\\TestPrintCmdHardCodedValues.cel");
            }

            Assert.IsTrue(CelesteStack.GlobalScope.VariableExists("print"));
            
            // This should definitely exist!
            using (StreamReader reader = new StreamReader(outputFilePath))
            {
                Assert.AreEqual("hello", reader.ReadLine());
                Assert.AreEqual("True", reader.ReadLine());
                Assert.AreEqual("1", reader.ReadLine());
                Assert.AreEqual("-1", reader.ReadLine());
            }
        }

        [TestMethod]
        public void TestPrintCmdVariables()
        {
            // Overwrite the file - we do not want any previous test results interacting with this
            string outputFilePath = CelesteScriptManager.ScriptDirectoryPath + "\\CoreScriptCommands\\PrintCmd\\TestPrintCmdVariables.txt";
            using (StreamWriter writer = new StreamWriter(outputFilePath, false))
            {
                Console.SetOut(writer);
                CelesteScript script = RunScript("CoreScriptCommands\\PrintCmd\\TestPrintCmdVariables.cel");
            }

            Assert.IsTrue(CelesteStack.GlobalScope.VariableExists("print"));

            // This should definitely exist!
            using (StreamReader reader = new StreamReader(outputFilePath))
            {
                Assert.AreEqual("hello", reader.ReadLine());
            }
        }
    }
}