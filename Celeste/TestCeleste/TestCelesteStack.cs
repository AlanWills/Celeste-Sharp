using Microsoft.VisualStudio.TestTools.UnitTesting;
using Celeste;
using System.Collections.Generic;
using UnitTestGameFramework;

namespace TestCeleste
{
    [TestClass]
    public class TestCelesteStack
    {
        [TestMethod]
        public void TestCelesteStackPopNumbers()
        {
            CelesteStack.Push(5);
            CelesteStack.Push(-1.5f);

            Assert.AreEqual(2, CelesteStack.StackSize);
            Assert.AreEqual(-1.5f, CelesteStack.Pop().As<float>());
            Assert.AreEqual(5, CelesteStack.Pop().As<float>());
        }

        [TestMethod]
        public void TestCelesteStackPopBooleans()
        {
            CelesteStack.Push(true);
            CelesteStack.Push(false);

            Assert.AreEqual(2, CelesteStack.StackSize);
            Assert.AreEqual(false, CelesteStack.Pop().As<bool>());
            Assert.AreEqual(true, CelesteStack.Pop().As<bool>());
        }

        [TestMethod]
        public void TestCelesteStackPopStrings()
        {
            CelesteStack.Push("test");
            CelesteStack.Push("");
            CelesteStack.Push("abcdefghijklmnopqrstuvwxyz");

            Assert.AreEqual(3, CelesteStack.StackSize);
            Assert.AreEqual("abcdefghijklmnopqrstuvwxyz", CelesteStack.Pop().As<string>());
            Assert.AreEqual("", CelesteStack.Pop().As<string>());
            Assert.AreEqual("test", CelesteStack.Pop().As<string>());
        }

        [TestMethod]
        public void TestCelesteStackPopLists()
        {
            CelesteStack.Push(new List<string>() { "test", "test1" });
            CelesteStack.Push(new List<float>() { 5, -10, 20 });
            CelesteStack.Push(new List<bool>() { true, true, false, false });
            CelesteStack.Push(new List<object>());

            Assert.AreEqual(4, CelesteStack.StackSize);
            TestListHelperFunctions.CheckOrderedListsEqual(new List<object>() { }, CelesteStack.Pop().As<List<object>>());
            TestListHelperFunctions.CheckOrderedListsEqual(new List<bool>() { true, true, false, false }, CelesteStack.Pop().As<List<bool>>());
            TestListHelperFunctions.CheckOrderedListsEqual(new List<float>() { 5, -10, 20 }, CelesteStack.Pop().As<List<float>>());
            TestListHelperFunctions.CheckOrderedListsEqual(new List<string>() { "test", "test1", }, CelesteStack.Pop().As<List<string>>());
        }
    }
}
