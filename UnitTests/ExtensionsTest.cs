using ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.Linq;

namespace UnitTests
{
    /// <summary>
    ///This is a test class for ExtensionsTest and is intended
    ///to contain all ExtensionsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ExtensionsTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        /// <summary>
        ///A test for ConvertToBinary
        ///</summary>
        [TestMethod()]
        public void ConvertTypeToBinaryTest()
        {
            TaskType type = TaskType.Development; // TODO: Initialize to an appropriate value
            Binary expected = new Binary(new byte[] { 0, 0, 1 });
            Binary actual;
            actual = Extensions.ConvertToBinary(type);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertToBinary
        ///</summary>
        [TestMethod()]
        public void ConvertStateToBinaryTest()
        {
            TaskState state = TaskState.In_Progress;
            Binary expected = new Binary(new byte[] { 0, 0, 1 });
            Binary actual;
            actual = Extensions.ConvertToBinary(state);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertToInt
        ///</summary>
        [TestMethod()]
        public void ConvertToIntTest()
        {
            Binary bin = new Binary(new byte[] { 0, 0, 4 });
            int expected = 4;
            int actual;
            actual = Extensions.ConvertToInt(bin);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsNumeric
        ///</summary>
        [TestMethod()]
        public void IsNonNumericTest()
        {
            string text = "123";
            bool expected = false;
            bool actual;
            actual = Extensions.IsNonNumeric(text);
            Assert.AreEqual(expected, actual);

            text = "-123";
            expected = true;
            actual = Extensions.IsNonNumeric(text);
            Assert.AreEqual(expected, actual);

            text = "12aab3";
            expected = true;
            actual = Extensions.IsNonNumeric(text);
            Assert.AreEqual(expected, actual);
        }
    }
}
