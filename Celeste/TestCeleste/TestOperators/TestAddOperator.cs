using Celeste;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TestCeleste
{
    [TestClass]
    public class TestAddOperator : CelesteUnitTest
    {
        //[TestMethod]
        //public void TestAddOperatorAddNumbers()
        //{
        //    CelesteScript script = new CelesteScript("TestScripts\\Operators\\Add\\TestAddOperatorNumbers.cel");
        //    script.Run();

        //    CelesteTestUtils.CheckStackSize(2);
        //    CelesteTestUtils.CheckStackResult(10.0f);
        //    CelesteTestUtils.CheckStackResult(15.0f);
        //}

        //[TestMethod]
        //public void TestAddOperatorAddStrings()
        //{
        //    CelesteScript script = new CelesteScript("TestScripts\\Operators\\Add\\TestAddOperatorStrings.cel");
        //    script.Run();

        //    CelesteTestUtils.CheckStackSize(1);
        //    CelesteTestUtils.CheckStackResult("testadding");
        //}

        //[TestMethod]
        //public void TestAddOperatorAddLists()
        //{
        //    CelesteScript script = new CelesteScript("TestScripts\\Operators\\Add\\TestAddOperatorLists.cel");
        //    script.Run();

        //    CelesteTestUtils.CheckStackSize(0);

        //    List<object> expected = new List<object>()
        //    {
        //        5.0f,
        //        "Test",
        //        true,
        //        10.0f,
        //        "List Adding",
        //        false
        //    };

        //    CelesteTestUtils.CheckStackResultList(expected);
        //    CelesteTestUtils.CheckLocalVariableList(script, "addedList", expected);
        //}
    }
}
