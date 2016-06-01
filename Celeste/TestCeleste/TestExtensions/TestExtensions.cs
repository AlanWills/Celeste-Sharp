using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celeste
{
    /// <summary>
    /// Summary description for TestExtensions
    /// </summary>
    [TestClass]
    public class TestExtensions
    {
        #region Properties and Fields

        object intObject = 1;
        object floatObject = 1.0f;
        object boolObject = true;
        object charObject = 's';
        object stringObject = "s";

        #endregion
        
        [TestMethod]
        public void TestExtensionsIsNumber()
        {
            Assert.IsTrue(intObject.IsNumber());
            Assert.IsTrue(floatObject.IsNumber());
            Assert.IsFalse(boolObject.IsNumber());
            Assert.IsFalse(charObject.IsNumber());
            Assert.IsFalse(stringObject.IsNumber());
        }

        [TestMethod]
        public void TestExtensionsIsString()
        {
            Assert.IsFalse(intObject.IsString());
            Assert.IsFalse(floatObject.IsString());
            Assert.IsFalse(boolObject.IsString());
            Assert.IsFalse(charObject.IsString());
            Assert.IsTrue(stringObject.IsString());
        }

        [TestMethod]
        public void TestExtensionsIsBool()
        {
            Assert.IsFalse(intObject.IsBool());
            Assert.IsFalse(floatObject.IsBool());
            Assert.IsTrue(boolObject.IsBool());
            Assert.IsFalse(charObject.IsBool());
            Assert.IsFalse(stringObject.IsBool());
        }

        [TestMethod]
        public void TestExtensionsValueEquals()
        {
            Assert.IsTrue(intObject.ValueEquals(floatObject));
            Assert.IsFalse(boolObject.ValueEquals(intObject));
            Assert.IsFalse(charObject.ValueEquals(stringObject));
            Assert.IsFalse(stringObject.ValueEquals(boolObject));
            Assert.IsTrue(stringObject.ValueEquals(stringObject));     // strings are not primitive types, but this should still work - we must have reflexivity
        }

        [TestMethod]
        public void TestExtensionsValueEqualsReflexivity()
        {
            // An object should ALWAYS equal itself (reference OR value)
            Assert.IsTrue(intObject.ValueEquals(intObject));
            Assert.IsTrue(floatObject.ValueEquals(floatObject));
            Assert.IsTrue(boolObject.ValueEquals(boolObject));
            Assert.IsTrue(charObject.ValueEquals(charObject));
            Assert.IsTrue(stringObject.ValueEquals(stringObject));
        }
    }
}
