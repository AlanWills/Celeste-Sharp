using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste
{
    [TestClass]
    public class TestSingleLineCommenting : CelesteUnitTest
    {
        [TestMethod]
        public void Test_SingleLineCommenting_Parsing()
        {
            CelesteScript script = RunScript("TokenModifiers\\TestSingleLineCommenting.cel");

            Assert.AreEqual(1, script.ScriptScope.VariableCount);
            script.CheckLocalVariable("var", 5.0f);
        }

        [TestMethod]
        public void Test_SingleLineCommenting_CommentInString()
        {
            CelesteScript script = RunScript("TokenModifiers\\TestSingleLineCommentingCommentInString.cel");

            Assert.AreEqual(1, script.ScriptScope.VariableCount);
            script.CheckLocalVariable("stringVar", "//test");
        }
    }
}