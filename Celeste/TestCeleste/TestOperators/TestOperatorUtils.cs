using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCeleste
{
    public static class TestOperatorUtils
    {
        public static void CheckStackSize(int expected)
        {
            Assert.AreEqual(expected, CelesteStack.StackSize);
        }

        public static void CheckStackResult<T>(T expected)
        {
            CelesteObject actual = CelesteStack.Pop();
            Assert.AreEqual(expected, actual.As<T>());
        }
    }
}
