using ExamPrep.Work;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ExamPrep.Testing
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            FirstClass f = new FirstClass();

            f.Testing = 4;

            Assert.AreEqual(4, f.Testing);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TestMethod2()
        {
            FirstClass f = new FirstClass();

            f.Something();
        }
    }
}
