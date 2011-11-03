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

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///A test for ConvertToBinary
        ///</summary>
        [TestMethod()]
        public void ConvertTypeToBinaryTest()
        {
            TaskType type = new TaskType(); // TODO: Initialize to an appropriate value
            Binary expected = null; // TODO: Initialize to an appropriate value
            Binary actual;
            actual = Extensions.ConvertToBinary(type);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ConvertToBinary
        ///</summary>
        [TestMethod()]
        public void ConvertStateToBinaryTest()
        {
            TaskState state = new TaskState(); // TODO: Initialize to an appropriate value
            Binary expected = null; // TODO: Initialize to an appropriate value
            Binary actual;
            actual = Extensions.ConvertToBinary(state);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ConvertToInt
        ///</summary>
        [TestMethod()]
        public void ConvertToIntTest()
        {
            Binary bin = null; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = Extensions.ConvertToInt(bin);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsNumeric
        ///</summary>
        [TestMethod()]
        public void IsNumericTest()
        {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = Extensions.IsNumeric(text);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
