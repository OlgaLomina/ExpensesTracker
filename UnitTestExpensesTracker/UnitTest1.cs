using Microsoft.VisualStudio.TestTools.UnitTesting;

using ExpensesTracker;

namespace UnitTestExpensesTracker
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ExpensesTracker.Class1 t = new ExpensesTracker.Class1();
            {
                Assert.IsTrue(t.TestMethod(5));
                Assert.IsFalse(t.TestMethod(0));
                Assert.IsFalse(t.TestMethod(1000));
                Assert.IsTrue(t.TestMethod(7));

            }

        }
    }
}
