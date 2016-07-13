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
        public void TestCelesteScriptDotCelFileOpen()
        {
            string filePath = "TestEmptyScript.cel";
            string dir = Celeste.Cel.ScriptDirectoryPath;
            string fullPath = Path.Combine(Celeste.Cel.ScriptDirectoryPath, filePath);
            Assert.IsTrue(File.Exists(fullPath));

            FileStream reader = File.Open(fullPath, FileMode.Open);
            Assert.IsNotNull(reader);

            reader.Close();
        }

        [TestMethod]
        public void TestCelesteScriptScopeCleanup()
        {
            CelesteScript script = RunScript("TestEmptyScript.cel");

            Assert.IsFalse(CelesteStack.Scopes.Exists(x => x == script.ScriptScope));
        }

        [TestMethod]
        public void TestCelesteScriptStackCleanup()
        {
            // We need to run a script which actually uses the stack rather than the empty one
            CelesteScript script = RunScript("TestReferencingScopedVariables.cel");

            Assert.AreEqual(0, CelesteStack.StackSize);
        }

        //[TestMethod]
        //public void TestCelesteScriptRecompilingOnChange()
        //{
        //    // We run a script, edit it and then see that it is automatically re-compiled
        //    CelesteScriptManager.RecompileScriptsWhenChanged = true;
        //    CelesteScript script = RunScript("TestRecompilingOnChange.cel");

        //    script.CheckLocalVariable("variable", 10.0f);

        //    using (StreamWriter writer = new StreamWriter(CelesteScriptManager.ScriptDirectoryPath + "\\TestRecompilingOnChange.cel", false))
        //    {
        //        string newScript = "scoped variable = 5";
        //        writer.WriteLine(newScript);
        //    }

        //    // This change will trigger a rerun - we need to wait for the event to be fired
        //    Thread.Sleep(2000);

        //    script.CheckLocalVariable("variable", 5.0f);    // Check the new value

        //    // Change the flag back before we revert our changes
        //    CelesteScriptManager.RecompileScriptsWhenChanged = false;

        //    // Rewrite the old script again to preserve this unit test
        //    using (StreamWriter writer = new StreamWriter(CelesteScriptManager.ScriptDirectoryPath + "\\TestRecompilingOnChange.cel", false))
        //    {
        //        string newScript = "scoped variable = 10";
        //        writer.WriteLine(newScript);
        //    }
        //}
    }
}
