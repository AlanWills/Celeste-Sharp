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
            string outputFilePath = Cel.ScriptDirectoryPath + "\\ScriptCommands\\Output\\PrintCmd\\TestPrintCmdHardCodedValues.txt";
            using (StreamWriter writer = new StreamWriter(outputFilePath, false))
            {
                Console.SetOut(writer);
                CelesteScript script = RunScript("ScriptCommands\\Output\\PrintCmd\\TestPrintCmdHardCodedValues.cel");
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
            string outputFilePath = Cel.ScriptDirectoryPath + "\\ScriptCommands\\Output\\PrintCmd\\TestPrintCmdVariables.txt";
            using (StreamWriter writer = new StreamWriter(outputFilePath, false))
            {
                Console.SetOut(writer);
                CelesteScript script = RunScript("ScriptCommands\\Output\\PrintCmd\\TestPrintCmdVariables.cel");
            }

            Assert.IsTrue(CelesteStack.GlobalScope.VariableExists("print"));

            // This should definitely exist!
            using (StreamReader reader = new StreamReader(outputFilePath))
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