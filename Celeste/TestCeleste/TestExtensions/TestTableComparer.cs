using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TestCeleste
{
    [TestClass]
    public class TestTableComparer
    {
        #region Properties and Fields

        KeyValuePair<object, object> intPair = new KeyValuePair<object, object>(1, null);
        KeyValuePair<object, object> floatPair = new KeyValuePair<object, object>(1.0f, null);
        KeyValuePair<object, object> boolPair = new KeyValuePair<object, object>(true, null);
        KeyValuePair<object, object> charPair = new KeyValuePair<object, object>('s', null);
        KeyValuePair<object, object> stringPair = new KeyValuePair<object, object>("s", null);
        KeyValuePair<object, object> listPair = new KeyValuePair<object, object>(new List<object>(), null);
        
        #endregion

        [TestMethod]
        public void TestTableComparerEquals()
        {
            TableKeyComparer keyComparer = new TableKeyComparer();

            Assert.IsTrue(keyComparer.Equals(intPair, new KeyValuePair<object, object>(1, null)));
            Assert.IsTrue(keyComparer.Equals(floatPair, new KeyValuePair<object, object>(1.0f, null)));
            Assert.IsTrue(keyComparer.Equals(boolPair, new KeyValuePair<object, object>(true, null)));
            Assert.IsTrue(keyComparer.Equals(charPair, new KeyValuePair<object, object>('s', null)));
            Assert.IsTrue(keyComparer.Equals(stringPair, new KeyValuePair<object, object>("s", null)));
            Assert.IsTrue(keyComparer.Equals(listPair, new KeyValuePair<object, object>(new List<object>(), null)));
        }
    }
}
