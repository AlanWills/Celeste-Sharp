using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading;

namespace TestCeleste
{
    [TestClass]
    public class TestCelesteScript : CelesteUnitTest
    {
        [TestMethod]
        public void Test_CelesteScript_DotCelFileOpen()
        {
            string filePath = "TestEmptyScript.cel";
            string dir = Cel.ScriptDirectoryPath;
            string fullPath = Path.Combine(Cel.ScriptDirectoryPath, filePath);
            Assert.IsTrue(File.Exists(fullPath));

            FileStream reader = File.Open(fullPath, FileMode.Open);
            Assert.IsNotNull(reader);

            reader.Close();
        }

        [TestMethod]
        public void Test_CelesteScript_ScopeCleanup()
        {
            CelesteScript script = RunScript("TestEmptyScript.cel");

            Assert.IsFalse(CelesteStack.Scopes.Exists(x => x == script.ScriptScope));
        }

        [TestMethod]
        public void Test_CelesteScript_StackCleanup()
        {
            // We need to run a script which actually uses the stack rather than the empty one
            CelesteScript script = RunScript("TestReferencingScopedVariables.cel");

            Assert.AreEqual(0, CelesteStack.StackSize);
        }

        [TestMethod]
        public void Test_CelesteScript_CurrentRunningScript()
        {
            // We need to run a script which actually uses the stack rather than the empty one
            RunScript("TestReferencingScopedVariables.cel");

            Assert.AreEqual(0, CelesteStack.StackSize);
            Assert.AreEqual("TestReferencingScopedVariables.cel", CelesteScript.CurrentRunningScript);
        }
    }
}
