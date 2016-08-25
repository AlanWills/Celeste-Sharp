using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Celeste;
using System.Collections.Generic;

namespace TestCeleste
{
    [TestClass]
    public class TestObjectComparer
    {
        #region Properties and Fields

        object intObject = 1;
        object floatObject = 1.0f;
        object boolObject = true;
        object charObject = 's';
        object stringObject = "s";
        object listObject = new List<object>();
        object secondListObject = new List<object>();

        #endregion

        [TestMethod]
        public void TestObjectComparerEquals()
        {
            ObjectComparer comparer = new ObjectComparer();

            Assert.IsTrue(comparer.Equals(intObject, floatObject));
            Assert.IsTrue(comparer.Equals(stringObject, stringObject));
            Assert.IsTrue(comparer.Equals(listObject, listObject));
            Assert.IsTrue(comparer.Equals(listObject, secondListObject));

            Assert.IsFalse(comparer.Equals(intObject, boolObject));
            Assert.IsFalse(comparer.Equals(charObject, stringObject));
            Assert.IsFalse(comparer.Equals(listObject, new List<object>() { true }));
        }
    }
}
