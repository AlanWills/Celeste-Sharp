using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace TestCeleste
{
    [TestClass]
    public class TestCelesteScript : CelesteUnitTest
    {
        [TestMethod]
        public void TestCelesteScriptDotCelFileOpen()
        {
            string filePath = "TestScripts\\TestEmptyScript.cel";
            string dir = Directory.GetCurrentDirectory();
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filePath);
            Assert.IsTrue(File.Exists(fullPath));

            FileStream reader = File.Open(fullPath, FileMode.Open);
            Assert.IsNotNull(reader);

            reader.Close();
        }

        [TestMethod]
        public void TestCelesteScriptScopeCleanup()
        {
            CelesteScript script = RunScript("TestScripts\\TestEmptyScript.cel");

            Assert.IsFalse(CelesteStack.Scopes.Exists(x => x == script.ScriptScope));
        }

        [TestMethod]
        public void TestCelesteScriptStackCleanup()
        {
            // We need to run a script which actually uses the stack rather than the empty one
            CelesteScript script = RunScript("TestScripts\\TestReferencingScopedVariables.cel");

            Assert.AreEqual(0, CelesteStack.StackSize);
        }
    }
}
