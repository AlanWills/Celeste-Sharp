using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste
{
    [TestClass]
    public class TestReferencing : CelesteUnitTest
    {
        [TestMethod]
        public void TestReferencingCelesteObjectAffectsNumberVariable()
        {
            Reference obj = new Reference(5.0f);
            CelesteObject celesteObject = new CelesteObject(obj);

            Assert.AreEqual(5.0f, celesteObject.Value);

            obj.Value = 10.0f;
            Assert.AreEqual(10.0f, celesteObject.Value);
        }

        [TestMethod]
        public void TestReferencingCelesteObjectAffectsStringVariable()
        {
            Reference obj = new Reference("Test");
            CelesteObject celesteObject = new CelesteObject(obj);

            Assert.AreEqual("Test", celesteObject.Value);

            obj.Value = "Test Change";
            Assert.AreEqual("Test Change", celesteObject.Value);
        }

        // Do unit tests with different local variables referencing each other
    }
}
