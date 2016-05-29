using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TestCeleste
{
    [TestClass]
    public class TestCelesteTable
    {
        [TestMethod]
        public void TestCelesteTableConstructor()
        {
            CelesteTable celesteTable = new CelesteTable();
            Assert.IsNotNull(celesteTable);
        }

        [TestMethod]
        public void TestCelesteTableAdd()
        {
            CelesteTable celesteTable = new CelesteTable();

            celesteTable.Add("key", "value");
            celesteTable.Add(10, 20);
            celesteTable.Add(new List<string>(), true);
            celesteTable.Add(new CelesteObject("test"), null);
            celesteTable.Add("table", new CelesteTable());
        }

        [TestMethod]
        public void TestCelesteTableGet()
        {
            CelesteTable celesteTable = new CelesteTable();

            celesteTable.Add("key", "value");
            celesteTable.Add(10, 20);

            List<string> listKey = new List<string>();
            celesteTable.Add(listKey, true);

            CelesteObject objectKey = new CelesteObject("test");
            celesteTable.Add(objectKey, "test");

            CelesteTable expectedTable = new CelesteTable();
            celesteTable.Add("table", expectedTable);

            Assert.AreEqual("value", celesteTable.Get<string>("key"));
            Assert.AreEqual(20, celesteTable.Get<float>(10));
            Assert.AreEqual(true, celesteTable.Get<bool>(listKey));
            Assert.AreEqual("test", celesteTable.Get<object>(objectKey));
            Assert.AreEqual(expectedTable, celesteTable.Get<CelesteTable>("table"));
        }
    }
}
