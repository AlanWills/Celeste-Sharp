using Microsoft.VisualStudio.TestTools.UnitTesting;
using Celeste;
using BindingPair = System.Tuple<System.Type, System.Type>;

namespace TestCeleste
{
    [TestClass]
    public class TestBindingsComparer
    {
        [TestMethod]
        public void Test_BindingsComparer_FloatEquality()
        {
            Assert.IsTrue(TestEquality(10.0f, -10.0f));
        }

        [TestMethod]
        public void Test_BindingsComparer_StringEquality()
        {
            Assert.IsTrue(TestEquality("string1", "string2"));
        }

        [TestMethod]
        public void Test_BindingsComparer_BoolEquality()
        {
            Assert.IsTrue(TestEquality(true, false));
        }

        [TestMethod]
        public void Test_BindingsComparer_BoolStringEquality()
        {
            Assert.IsTrue(TestEquality(true, "string"));
        }

        [TestMethod]
        public void Test_BindingsComparer_ClassEquality()
        {
            Assert.IsTrue(TestEquality(new BindingsComparer(), new BindingsComparer()));
        }
        
        /// <summary>
        /// Wrapper test function which creates two comparers which should be equal.
        /// Uses the generic type of the input as well as some actual values to obtain the relevant types for the comparers.
        /// Although this is not really necessary, I feel like it provides testing for actual use case which is obtaining types from values.
        /// Unit test assert checking is left to the caller of this function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <returns></returns>
        private bool TestEquality<T, K>(T input1, K input2)
        {
            BindingsComparer comparer = new BindingsComparer();

            BindingPair pairOne = new BindingPair(input1.GetType(), input2.GetType());
            BindingPair pairTwo = new BindingPair(typeof(T), typeof(K));

            return comparer.Equals(pairOne, pairTwo);
        }

        [TestMethod]
        public void Test_BindingsComparer_BoolFloatInequality()
        {
            Assert.IsTrue(TestInequality(true, 10.0f));
        }

        [TestMethod]
        public void Test_BindingsComparer_StringBoolInequality()
        {
            Assert.IsTrue(TestInequality("string", false));
        }

        [TestMethod]
        public void Test_BindingsComparer_ClassFloatInequality()
        {
            Assert.IsTrue(TestInequality(new BindingsComparer(), 10.0f));
        }

        /// <summary>
        /// Wrapper test function which creates two comparers which should be not equal.
        /// Uses the generic type of the input as well as some actual values to obtain the relevant types for the comparers.
        /// Although this is not really necessary, I feel like it provides testing for actual use case which is obtaining types from values.
        /// Unit test assert checking is left to the caller of this function.
        /// The inputs cannot be of the same type otherwise this test will obviously fail!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <returns></returns>
        private bool TestInequality<T, K>(T input1, K input2)
        {
            Assert.IsTrue(input1.GetType() != input2.GetType());
            Assert.IsTrue(typeof(T) != typeof(K));

            BindingsComparer comparer = new BindingsComparer();

            BindingPair pairOne = new BindingPair(input1.GetType(), typeof(T));
            BindingPair pairTwo = new BindingPair(input2.GetType(), typeof(K));

            return !comparer.Equals(pairOne, pairTwo);
        }
    }
}
