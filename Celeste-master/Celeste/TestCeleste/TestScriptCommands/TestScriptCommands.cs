using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace TestCeleste
{
    [TestClass]
    public class TestScriptCommands : CelesteUnitTest
    {
        [TestMethod]
        public void TestScriptCommandsScriptCommandReassignmentAndRestoration()
        {
            // Overwrite the file - we do not want any previous test results interacting with this
            string outputFilePath = Cel.ScriptDirectoryPath + "\\ScriptCommands\\Core\\TestScriptCommandsScriptCommandReassignmentAndRestoration.txt";
            CelesteScript script;

            using (StreamWriter writer = new StreamWriter(outputFilePath, false))
            {
                Console.SetOut(writer);
                script = RunScript("ScriptCommands\\Core\\TestScriptCommandsScriptCommandReassignmentAndRestoration.cel");
            }

            script.CheckLocalVariable("reassigned", "success");

            using (StreamReader reader = new StreamReader(outputFilePath))
            {
                Assert.AreEqual("success", reader.ReadLine());
            }
        }
    }
}
