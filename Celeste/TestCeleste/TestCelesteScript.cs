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
            CelesteScript script = new CelesteScript("TestScripts\\TestEmptyScript.cel");
            script.Run();

            Assert.IsFalse(CelesteStack.Scopes.Exists(x => x == script.ScriptScope));
        }
    }
}
