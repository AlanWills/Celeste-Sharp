using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using UnitTestGameFramework;

namespace TestCeleste
{
    /// <summary>
    /// Summary description for TestCelesteObject_
    /// </summary>
    [TestClass]
    public class TestCelesteObject
    {
        #region Properties and Fields

        private CelesteObject CelesteObjectNumber { get; set; }
        private CelesteObject CelesteObjectBool { get; set; }
        private CelesteObject CelesteObjectChar { get; set; }
        private CelesteObject CelesteObjectString { get; set; }
        private CelesteObject CelesteObjectStringList { get; set; }
        private CelesteObject CelesteObjectCelesteObjectList { get; set; }
        private CelesteObject CelesteTable { get; set; }

        #endregion

        public TestCelesteObject()
        {

        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
         [TestInitialize()]
        public void MyTestInitialize()
        {
            CelesteObjectNumber = new CelesteObject(5);
            CelesteObjectBool = new CelesteObject(true);
            CelesteObjectChar = new CelesteObject('[');
            CelesteObjectString = new CelesteObject("Test");
            CelesteObjectStringList = new CelesteObject(new List<string>() { "1", "2" });

            List<CelesteObject> objects = new List<CelesteObject>()
            {
                CelesteObjectNumber,
                CelesteObjectBool,
                CelesteObjectString,
                CelesteObjectStringList,
            };

            CelesteObjectCelesteObjectList = new CelesteObject(objects);
            CelesteTable = new CelesteObject(new Dictionary<object, object>());
        }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestCelesteObjectConstructor()
        {
            CelesteObjectNumber = new CelesteObject(5);
            CelesteObjectBool = new CelesteObject(true);
            CelesteObjectChar = new CelesteObject('[');
            CelesteObjectString = new CelesteObject("Test");
            CelesteObjectStringList = new CelesteObject(new List<string>() { "1", "2" });
        }

        [TestMethod]
        public void TestCelesteObjectIsNumber()
        {
            Assert.IsTrue(CelesteObjectNumber.IsNumber());
            Assert.IsFalse(CelesteObjectBool.IsNumber());
            Assert.IsFalse(CelesteObjectChar.IsNumber());
            Assert.IsFalse(CelesteObjectString.IsNumber());
            Assert.IsFalse(CelesteObjectStringList.IsNumber());
            Assert.IsFalse(CelesteObjectCelesteObjectList.IsNumber());
            Assert.IsFalse(CelesteTable.IsNumber());
        }

        [TestMethod]
        public void TestCelesteObjectIsString()
        {
            Assert.IsFalse(CelesteObjectNumber.IsString());
            Assert.IsFalse(CelesteObjectBool.IsString());
            Assert.IsFalse(CelesteObjectChar.IsString());
            Assert.IsTrue(CelesteObjectString.IsString());
            Assert.IsFalse(CelesteObjectStringList.IsString());
            Assert.IsFalse(CelesteObjectCelesteObjectList.IsString());
            Assert.IsFalse(CelesteTable.IsString());
        }

        [TestMethod]
        public void TestCelesteObjectIsList()
        {
            Assert.IsFalse(CelesteObjectNumber.IsList());
            Assert.IsFalse(CelesteObjectBool.IsList());
            Assert.IsFalse(CelesteObjectChar.IsList());
            Assert.IsFalse(CelesteObjectString.IsList());
            Assert.IsTrue(CelesteObjectStringList.IsList());
            Assert.IsTrue(CelesteObjectCelesteObjectList.IsList());
            Assert.IsFalse(CelesteTable.IsList());

            Assert.IsFalse(CelesteObjectNumber.IsList());
            Assert.IsFalse(CelesteObjectBool.IsList());
            Assert.IsFalse(CelesteObjectChar.IsList());
            Assert.IsFalse(CelesteObjectString.IsList());
            Assert.IsTrue(CelesteObjectStringList.IsList());
            Assert.IsTrue(CelesteObjectCelesteObjectList.IsList());
            Assert.IsFalse(CelesteTable.IsList());
        }

        [TestMethod]
        public void TestCelesteObjectIsTable()
        {
            Assert.IsFalse(CelesteObjectNumber.IsTable());
            Assert.IsFalse(CelesteObjectBool.IsTable());
            Assert.IsFalse(CelesteObjectChar.IsTable());
            Assert.IsFalse(CelesteObjectString.IsTable());
            Assert.IsFalse(CelesteObjectStringList.IsTable());
            Assert.IsFalse(CelesteObjectCelesteObjectList.IsTable());
            Assert.IsTrue(CelesteTable.IsTable());
        }

        [TestMethod]
        public void TestCelesteObjectAs()
        {
            Assert.AreEqual(5, CelesteObjectNumber.As<float>());
            Assert.AreEqual(true, CelesteObjectBool.As<bool>());
            Assert.AreEqual('[', CelesteObjectChar.As<char>());
            Assert.AreEqual("[", CelesteObjectChar.As<string>());
            Assert.AreEqual("Test", CelesteObjectString.As<string>());
            TestHelperFunctions.CheckOrderedListsEqual(new List<string>() { "1", "2" }, CelesteObjectStringList.As<List<string>>());

            List<CelesteObject> objects = new List<CelesteObject>()
            {
                CelesteObjectNumber,
                CelesteObjectBool,
                CelesteObjectString,
                CelesteObjectStringList,
            };
            TestHelperFunctions.CheckOrderedListsEqual(objects, CelesteObjectCelesteObjectList.As<List<CelesteObject>>());
        }
    }
}
